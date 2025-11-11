using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Metodo para obtener input de movimiento
    public Vector2 GetMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }
    // Metodo para comprobar si se esta atacando
    public bool isAttacking()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
