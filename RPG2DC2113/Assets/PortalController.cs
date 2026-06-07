using UnityEngine;
using UnityEngine.SceneManagement; 

public class PortalController : MonoBehaviour
{
    [SerializeField] private int sceneID;

    void OnTriggerEnter2D(Collider2D collision) //срабатывает при столкновении с объектом
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}
