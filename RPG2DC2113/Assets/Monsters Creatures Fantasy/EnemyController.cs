using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 3f;          
    [SerializeField] private float chaseRange = 7f;       
    [SerializeField] private float attackRange = 1.5f;      

    [Header("Настройки атаки")]
    [SerializeField] private int damage = 10;               
    [SerializeField] private float attackCooldown = 1.5f;   
    private float lastAttackTime;

    [Header("Ссылки")]
    public Transform player;              // Ссылка на игрока

    private Rigidbody2D rb;
    private Animator animator;

    public bool isDie;
    public float hp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) return;
        //развороты
        if (player.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (player.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

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
        animator.SetFloat("Speed", 1);
    }

    void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
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
        if (player.gameObject.GetComponent<PlayerAnimation>().isDie == false)
        {
           player.gameObject.GetComponent<PlayerController>().GetDamageToPlayer(damage);
           animator.SetTrigger("Attack");  
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void GetDamage(float damage)
    {
        if (isDie == true) return;

        hp -= damage;
        
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            isDie = true;
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
           animator.SetTrigger("Hurt"); 
        }
    }

}
