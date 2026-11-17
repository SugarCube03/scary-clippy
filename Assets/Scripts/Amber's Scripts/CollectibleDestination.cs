using UnityEngine;

public class CollectibleDestination : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private string nextLevel;

    // where does the collectible need to be dropped
    private Collider2D destinationCollider;

    void Awake()
    {
        destinationCollider = GetComponent<Collider2D>();
    }

    // are we ready to move on
    public void HandleDelivery(Vector2 dropPosition)
    {
        if (levelManager.AreObjectivesComplete() && IsInside(dropPosition, destinationCollider))
        {
            gameManager.LoadNextLevel(nextLevel);
        }
    }

    // is the the collectible dropped in the correct place
    public bool IsInside(Vector2 dropPosition, Collider2D destinationCollider)
    {
        return destinationCollider.OverlapPoint(dropPosition);
    }
}


