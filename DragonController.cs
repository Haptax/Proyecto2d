using UnityEngine;
using UnityEngine.SceneManagement; // al inicio del script

public class DragonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject firePrefab;
    public Transform firePoint;
    public GameObject attackHitbox;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isGrounded = true;
    private int jumpCount = 0;
    private bool isFlying = false;
    private bool facingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Volteo y ajuste de firePoint y attackHitbox según dirección
        bool faceLeft = move < 0;
        if (faceLeft != facingLeft && move != 0)
        {
            facingLeft = faceLeft;

            sr.flipX = facingLeft;

            // Ajustar firePoint para que esté a la izquierda o derecha
            Vector3 firePos = firePoint.localPosition;
            firePos.x = Mathf.Abs(firePos.x) * (facingLeft ? -1 : 1);
            firePoint.localPosition = firePos;

            // Ajustar rotación del firePoint para que el fuego salga hacia adelante
            firePoint.localRotation = Quaternion.Euler(0, facingLeft ? 180 : 0, 0);

            // Ajustar posición del attackHitbox para que coincida con el lado
            if (attackHitbox != null)
            {
                Vector3 hitboxPos = attackHitbox.transform.localPosition;
                hitboxPos.x = Mathf.Abs(hitboxPos.x) * (facingLeft ? -1 : 1);
                attackHitbox.transform.localPosition = hitboxPos;
            }
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded || jumpCount < 2)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
                isGrounded = false;
                animator.SetTrigger("Jump");
            }
        }

        // Vuelo (doble flecha ↑)
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount >= 2)
        {
            isFlying = true;
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            animator.SetBool("Flying", true);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && isFlying)
        {
            isFlying = false;
            rb.gravityScale = 1;
            animator.SetBool("Flying", false);
        }

        // Ataque físico
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
            attackHitbox.SetActive(true);
            Invoke("DisableHitbox", 0.2f); // Desactiva el hitbox después de 0.2 segundos
        }

        // Ataque con fuego
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Fire");
            Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    public void EnableHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }
    public void Die()
    {
        // Puedes poner animación de muerte aquí si quieres
        Debug.Log("Dragon muerto. Reiniciando juego...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
