using UnityEngine;

public class ScreenPiece : MonoBehaviour, IPickable
{
    public void PickUp(Transform player)
    {
        transform.SetParent(player);
        transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    public void PutDown()
    {
        transform.position = transform.parent.position;
        transform.SetParent(null);
    }
}