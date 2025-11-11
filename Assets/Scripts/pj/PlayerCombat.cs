using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float attackDuration = 0.6f;
    [SerializeField] private float attackCooldown = 0.8f;

    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;

    // Componentes
    private PlayerInput playerInput;
    private PlayerHealth playerHealth;

    // Estados públicos
    public bool IsAttacking { get; private set; }
    public bool CanAttack { get; private set; } = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();

        // Configurar layer de enemigos (debes crear la layer "Enemy" en Unity)
        enemyLayer = LayerMask.GetMask("Enemy");

        // Crear punto de ataque si no existe
        if (attackPoint == null)
        {
            CreateAttackPoint();
        }
    }

    private void CreateAttackPoint()
    {
        GameObject point = new GameObject("AttackPoint");
        attackPoint = point.transform;
        attackPoint.SetParent(transform);
        attackPoint.localPosition = new Vector3(0.5f, 0, 0);
    }

    private void Update()
    {
        // Solo atacar si puede atacar, no está atacando ya, y está vivo
        if (CanAttack && !IsAttacking && playerInput.isAttacking() && playerHealth.IsAlive())
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // INICIO DEL ATAQUE
        IsAttacking = true;
        CanAttack = false;

        Debug.Log("Iniciando ataque...");

        // ESPERAR MOMENTO DEL GOLPE (cuando el arma golpea)
        yield return new WaitForSeconds(attackDelay);

        // EJECUTAR DAÑO
        ExecuteAttack();

        // ESPERAR FIN DE ANIMACIÓN
        yield return new WaitForSeconds(attackDuration - attackDelay);

        // FIN DEL ATAQUE
        IsAttacking = false;

        // COOLDOWN
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void ExecuteAttack()
    {
        Debug.Log("Ejecutando daño en enemigos...");

        // Detectar todos los colliders en el área de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(damage);
                    Debug.Log($"¡Golpeado: {enemy.name} - Daño: {damage}!");
                }
            }
        }

        // Si no golpeó nada
        if (hitEnemies.Length == 0)
        {
            Debug.Log("Ataque al aire");
        }
    }

    // MÉTODO PARA LLAMAR DESDE EVENTOS DE ANIMACIÓN (OPCIONAL)
    // Esto te permite sincronizar el daño con frames específicos de la animación
    public void AnimationAttackHit()
    {
        // Solo ejecutar daño si está en medio de un ataque
        if (IsAttacking)
        {
            ExecuteAttack();
        }
    }

    // VISUALIZACIÓN EN EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}