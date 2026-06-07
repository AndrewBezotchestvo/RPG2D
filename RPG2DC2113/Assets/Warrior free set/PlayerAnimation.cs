using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float speed;
    public bool isAttack;
    public bool isHurt;
    public bool isDie;

    [SerializeField] public float timeAttack = 1f;
    [SerializeField] public float timeHurt = 1f;

    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {         
        speed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (isAttack)
        {
            time += Time.deltaTime;
            if (time >= timeAttack)
            {
                isAttack = false;
            }
        }

        if (isHurt)
        {
            time += Time.deltaTime;
            if (time >= timeHurt)
            {
                isHurt = false;
            }
        }

        if (rb.linearVelocityX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.linearVelocityX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Attack()
    {
        if (isHurt == false && isAttack == false && isDie == false)
        {        
            animator.SetTrigger("Attack");
            isAttack = true;
            time = 0;
        }
    }
    public void Hurt()
    {
        if (isHurt == false && isAttack == false && isDie == false)
        {        
            animator.SetTrigger("Hurt");
            isHurt = true;
            time = 0;
        }
    }
    public void Die()
    {
        if (isDie != true)
        {
             animator.SetTrigger("Death");
             isDie = true;
        }
    }

}
