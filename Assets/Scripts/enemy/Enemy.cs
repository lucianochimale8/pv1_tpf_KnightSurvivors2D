using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 30;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // El enemigo muere
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dañar al jugador si lo toca
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}
