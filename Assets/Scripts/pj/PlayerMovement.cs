using UnityEngine;

/*
 * Script para el movimiento del personaje
 */

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;

    // Propiedad publica para saber si se esta moviendo
    public bool IsMoving { get; private set; }

    // Awake para cargar los componentes cuando se inicia el proyecto
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    // FixedUpdate para controlar las fisicas
    private void FixedUpdate()
    {
        Vector2 input = playerInput.GetMovementInput();
        Move(input);
        Flip(input.x);

    }
    /*
     * Metodo para mover al personaje 
     * mediante un parametro que reconoce el input
     */
    private void Move(Vector2 input)
    {
        Vector2 movement = input.normalized;
        rb.linearVelocity = movement * speed;
        IsMoving = movement.magnitude > 0.1f;
    }
    /*
     * Metodo para reconocer para que lado 
     * esta dirigiendose el personaje para voltear la imagen
     */
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
