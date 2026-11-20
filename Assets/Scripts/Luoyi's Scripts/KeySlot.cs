using UnityEngine;

// Put the right item here to open a door.
// Needs: BoxCollider2D with "Is Trigger" ticked
public class KeySlot : MonoBehaviour
{
    public string requiredItemId = "key";   // which item this slot wants
    public GameObject door;                 // the door to open

    bool isFilled;   // already has the right item in it?

    public void TryAccept(ScreenPiece item)
    {
        if (isFilled) return;                        // already done
        if (item.itemId != requiredItemId) return;   // wrong item

        isFilled = true;
        item.transform.position = transform.position;      // snap the item into the slot
        item.GetComponent<Collider2D>().enabled = false;   // so Clippy can't pick it up again
        door.SetActive(false);                             // open the door
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.CompleteObjective();
        }
    }
}