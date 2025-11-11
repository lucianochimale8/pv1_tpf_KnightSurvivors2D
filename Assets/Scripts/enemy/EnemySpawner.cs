using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 3f;

    void Start()
    {
        // Generar enemigos cada X segundos
        InvokeRepeating("SpawnEnemy", 2f, spawnTime);
    }

    private void SpawnEnemy()
    {
        // Spawn cerca del jugador
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * 5f;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
