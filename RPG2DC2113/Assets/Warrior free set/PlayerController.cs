using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackDistance = 5;

    private bool isDie;
    private PlayerAnimation playerAnimation;
    public bool isAvalible;

    void Start()
    {
        isDie = false;
        isAvalible = true;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        isAvalible = !playerAnimation.isAttack && !playerAnimation.isHurt && !isDie; 

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetDamageToEnemy();
        }
        
    }

    public void GetDamageToEnemy()
    {
        if (isAvalible) 
        {
            playerAnimation.Attack();

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackDistance);
            
            foreach(Collider2D enemy in enemies)
            {
                if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    enemyController.GetDamage(damage);
                }
            }
        }
    } 

    public void GetDamageToPlayer(float damage)
    {
        if (isAvalible)
        {
            hp -= damage;

            if (hp <= 0)
            {
                playerAnimation.Die();
                isDie = true;
            }
            else
            {
                playerAnimation.Hurt();
            }
        }
    }
}
