using UnityEngine;
using UnityEngine.InputSystem;

// ---------------------------------------------------------------
// Interfaces
// Any object that wants to work with Clippy implements one of these.
// ---------------------------------------------------------------

// Things you press E on (pop-ups, buttons...)
public interface IInteractable
{
    void Interact();
}

// Things you can pick up and put down (screen pieces...)
public interface IPickable
{
    void PickUp(Transform player);
    void PutDown();
}

// ---------------------------------------------------------------
// Clippy
// Controls: A/D = move, Space = jump, E = pick up / put down / interact
// Needs: Rigidbody2D + BoxCollider2D on the same object
// ---------------------------------------------------------------

public class ClippyPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float interactRange = 1f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    IPickable heldItem;   // what Clippy is holding right now (null = nothing)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        // 1. Move left / right
        float x = 0f;
        if (kb.aKey.isPressed) x = -1f;
        if (kb.dKey.isPressed) x = 1f;
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // 2. Jump (only when standing on the ground)
        if (kb.spaceKey.wasPressedThisFrame && IsOnGround())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 3. Press E
        if (kb.eKey.wasPressedThisFrame)
        {
            PressE();
        }
    }

    // Shoot a short invisible line downward. If it hits the ground layer, we are on the ground.
    bool IsOnGround()
    {
        float distance = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        return Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
    }

    void PressE()
    {
        // Holding something? Put it down.
        if (heldItem != null)
        {
            heldItem.PutDown();
            heldItem = null;
            return;
        }

        // Otherwise look at everything inside a circle around Clippy.
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D thing in nearby)
        {
            IPickable pickable = thing.GetComponent<IPickable>();
            if (pickable != null)
            {
                pickable.PickUp(transform);
                heldItem = pickable;
                return;
            }

            IInteractable interactable = thing.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }
    }
}