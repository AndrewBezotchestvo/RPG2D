using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {   
        Vector2 dir = Vector2.zero;

        if (playerController.isAvalible == false) 
        {
            GetComponent<Rigidbody2D>().linearVelocity = dir;
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().linearVelocity = speed * dir;
    }
}
