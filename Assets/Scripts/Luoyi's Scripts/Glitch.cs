using UnityEngine;

// Touching this kills Clippy.
public class Glitch : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Glitch touched by: " + other.name);

    ClippyPlayer player = other.GetComponent<ClippyPlayer>();
    if (player != null)
    {
        player.Die();
    }
}
}