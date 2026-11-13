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
    public float wallJumpPush = 6f;  // how hard a wall jump pushes Clippy sideways
    public float wallJumpTime = 0.2f; // how long left/right keys are ignored after a wall jump
    public LayerMask groundLayer;
    public LayerMask wallLayer;   // walls Clippy can jump off

    Rigidbody2D rb;
    float wallJumpTimer;   // counts down after a wall jump
    IPickable heldItem;   // what Clippy is holding right now (null = nothing)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;
        wallJumpTimer -= Time.deltaTime;

        // 1. Move left / right
        // (skipped for a moment after a wall jump, so the sideways push isn't cancelled)
        if (wallJumpTimer <= 0f)
        {
            float x = 0f;
            if (kb.aKey.isPressed) x = -1f;
            if (kb.dKey.isPressed) x = 1f;
            rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
        }

        // 2. Jump (only when standing on the ground)
        if (kb.spaceKey.wasPressedThisFrame)
        {
            int wall = WallSide();

            if (IsOnGround())
            {
                // Normal jump: straight up
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (wall != 0)
            {
                // Wall jump: up, and away from the wall
                rb.linearVelocity = new Vector2(-wall * wallJumpPush, jumpForce);
                wallJumpTimer = wallJumpTime;
            }
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

    // Same idea, sideways. Returns -1 = wall on the left, 1 = wall on the right, 0 = no wall.
    int WallSide()
    {
        float distance = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        if (Physics2D.Raycast(transform.position, Vector2.left, distance, wallLayer)) return -1;
        if (Physics2D.Raycast(transform.position, Vector2.right, distance, wallLayer)) return 1;
        return 0;
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