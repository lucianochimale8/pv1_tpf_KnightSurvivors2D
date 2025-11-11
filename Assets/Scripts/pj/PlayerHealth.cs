using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Vida restante: " + health);

        if (health <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }
    public bool IsAlive()
    {
        return health > 0;
    }

    private IEnumerator DieCoroutine()
    {
        Debug.Log("Iniciando animación de muerte...");

        // 1. Activar la animación de muerte
        animator.SetTrigger("Die");

        // 2. Esperar a que termine la animación (ajusta este tiempo)
        yield return new WaitForSeconds(2f); // 2 segundos para la animación

        // 3. Hacer que desaparezca
        Debug.Log("Jugador eliminado");
        gameObject.SetActive(false);

        // O si prefieres destruirlo completamente:
        // Destroy(gameObject);
    }
}