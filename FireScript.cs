// Script para el fuego, por ejemplo "FireProjectile.cs"
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aldeano") || other.CompareTag("Casa"))
        {
            Destroy(other.gameObject); // destruye solo un objeto
            Destroy(gameObject); // destruye el fuego tras impactar
        }
    }
}
