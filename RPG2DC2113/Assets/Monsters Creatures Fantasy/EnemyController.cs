using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 3f;          
    public float chaseRange = 7f;       
    public float attackRange = 1.5f;      

    [Header("Настройки атаки")]
    public int damage = 10;               
    public float attackCooldown = 1.5f;   
    private float lastAttackTime;

    [Header("Ссылки")]
    public Transform player;              // Ссылка на игрока

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {

                TryAttack();
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Debug.Log("Враг атакует!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
