using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class ScreenPiece : MonoBehaviour, IPickable
{
    [SerializeField] private CollectibleDestination destination; // added by amber

    public void PickUp(Transform player)
    {
        transform.SetParent(player);
        transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    public void PutDown()
    {
        transform.position = transform.parent.position;
        transform.SetParent(null);
        destination.HandleDelivery(transform.position); // added by amber
    }
}