using UnityEngine;
/*
 * Script para controlar las animaciones del personaje
 * Se encarga de cambiar entre idle y run según el estado de movimiento
 */
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation();
    }
    /*
     * Método UpdateAnimation - Controla la transición entre animaciones
     * Lee el estado de movimiento y actualiza los parámetros del Animator
     */
    private void UpdateAnimation()
    {
        // Obtener el estado de movimiento del PlayerMovement
        bool isMoving = playerMovement.IsMoving;
        // Actualizar el parámetro en el Animator para transición entre animaciones
        animator.SetBool("isMoving", isMoving);
        // El Animator automáticamente manejará la transición entre:
        // - "isMoving" = false -> Animación IDLE
        // - "isMoving" = true -> Animación RUN
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Die");
        }*/
    }
}
