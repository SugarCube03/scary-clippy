using UnityEngine;

// An item Clippy can carry.
// Can be put into a KeySlot with the same id, or delivered to a CollectibleDestination.
// Needs: BoxCollider2D with "Is Trigger" ticked
public class ScreenPiece : MonoBehaviour, IPickable
{
    public string itemId = "key";                                  // added by Luoyi: used by KeySlot
    [SerializeField] private CollectibleDestination destination;   // added by Amber

    public void PickUp(Transform player)
    {
        transform.SetParent(player);                         // follow Clippy
        transform.localPosition = new Vector3(0f, 1f, 0f);   // sit on Clippy's head
    }

    public void PutDown()
    {
        transform.position = transform.parent.position;      // drop where Clippy is
        transform.SetParent(null);                           // stop following

        // added by Amber
        if (destination != null)
        {
            destination.HandleDelivery(transform.position);
        }

        // added by Luoyi: did we land on a key slot?
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D thing in nearby)
        {
            KeySlot slot = thing.GetComponent<KeySlot>();
            if (slot != null)
            {
                slot.TryAccept(this);
                return;
            }
        }
    }
}