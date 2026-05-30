using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float speed;
    public bool isAttack;
    public bool isHurt;
    public bool isDie;

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

        if (isAttack) animator.SetTrigger("Attack");
        if (isHurt) animator.SetTrigger("Hurt");
        if (isDie) animator.SetTrigger("Death");

        if (rb.linearVelocityX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.linearVelocityX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
