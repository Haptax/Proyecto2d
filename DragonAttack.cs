using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public float knockbackForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aldeano"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Determinar dirección del empuje
                Vector2 direction = (other.transform.position - transform.position).normalized;
                direction.y = 0.5f; // le da un pequeño salto

                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
