using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void FixedUpdate()
    {
        Vector2 input = playerInput.GetMovementInput();
        Move(input);
        Flip(input.x);

    }

    private void Move(Vector2 input)
    {
        Vector2 movement = input.normalized;
        rb.linearVelocity = movement * speed;
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0.1f)
        {
            spriteRenderer.flipX = false;

        } else if (horizontal < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
