using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class KnightController : MonoBehaviour
{
    public float speed = 2f;
    public Transform puntoA;
    public Transform puntoB;
    public GameObject gameOverUI;

    private bool goingToB = true;
    private bool gameEnded = false;
    
    void Update()
    {
        if (gameEnded) return;

        Patrol();
    }

    void Patrol()
    {
        float tolerance = 0.05f;
        Transform target = goingToB ? puntoB : puntoA;

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - target.position.x) <= tolerance)
        {
            goingToB = !goingToB;
        }

        // Flip sprite según dirección
        if (direction.x != 0)
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameEnded) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            gameEnded = true;

            // Opcional: Desactiva al dragón
            collision.gameObject.SetActive(false);
            if (gameOverUI != null)
                gameOverUI.SetActive(true);
            // Reinicia la escena después de 2 segundos
            Invoke(nameof(RestartGame), 2f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
