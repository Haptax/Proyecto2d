using UnityEngine;
using UnityEngine.UI;

public class PortalEscape : MonoBehaviour
{
    public GameObject victoryTextPrefab;

    private bool gameEnded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameEnded) return;

        if (other.CompareTag("Player")) // Asegúrate de que el dragón tenga el tag "Player"
        {
            gameEnded = true;
            ShowVictoryText(other.transform);
            Time.timeScale = 0f; // Pausa el juego
        }
    }

    void ShowVictoryText(Transform cameraTransform)
    {
        GameObject text = Instantiate(victoryTextPrefab);
        text.transform.SetParent(null); // Evita que se ligue a otro objeto
        text.transform.position = cameraTransform.position + new Vector3(0, 0, 0);
    }
}
