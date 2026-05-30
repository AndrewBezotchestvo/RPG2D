using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackDistance = 5;

    private bool isDie;
    private Animator animator;

    void Start()
    {
        isDie = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetDamageToEnemy();
        }
    }

    public void GetDamageToEnemy()
    {
        animator.SetTrigger("Attack");
    } 

    public void GetDamageToPlayer(float damage)
    {
        hp -= damage;
    }
}
