using UnityEngine;

public class VillagerAI : MonoBehaviour
{
    public float fleeSpeed = 3f;
    public float detectionRange = 5f;
    public float safeDistance = 10f;
    public Transform dragon;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFleeing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (dragon == null)
        {
            dragon = GameObject.FindWithTag("Player").transform; // Asegúrate de que el dragón tenga el tag "Player"
        }
    }

    void Update()
    {
        float distanceToDragon = Vector2.Distance(transform.position, dragon.position);

        if (distanceToDragon < detectionRange)
        {
            isFleeing = true;
        }
        else if (distanceToDragon > safeDistance)
        {
            isFleeing = false;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("IsRunning", false);
        }

        if (isFleeing)
        {
            Vector2 direction = (transform.position - dragon.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * fleeSpeed, rb.linearVelocity.y);

            // Girar al sentido correcto
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;

            animator.SetBool("IsRunning", true);
        }
    }
}
