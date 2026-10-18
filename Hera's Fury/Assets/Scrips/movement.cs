using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 65f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    //for player knockback
    private Rigidbody2D rb;
    private bool isBeingKnockedback = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 15f;
    [SerializeField] private float knockbackDuration = 0.2f;


    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

// Use this for initialization
void Start()
{
    t = transform;
    r2d = GetComponent<Rigidbody2D>();
    mainCollider = GetComponent<CapsuleCollider2D>();
    r2d.freezeRotation = true;
    r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    r2d.gravityScale = gravityScale;
    facingRight = t.localScale.x > 0;
    
    if (mainCamera)
    {
        cameraPos = mainCamera.transform.position;
    }
}
// Update is called once per frame
void Update()
{

        // Prevent player input from overriding physics during knockback
        if (isBeingKnockedback) return;

        // Movement controls
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    {
        moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
    }
    else
    {
        if (isGrounded || r2d.linearVelocity.magnitude < 0.01f)
        {
        moveDirection = 0;
        } 
    }
    // Change facing direction
    if (moveDirection != 0)
    {
        if (moveDirection > 0 && !facingRight)
        {
            facingRight = true;
            t.localScale = new Vector3(Mathf.Abs(t.localScale.x),
            t.localScale.y, transform.localScale.z);
        }
        if (moveDirection < 0 && facingRight) 
        {
            facingRight = false;
            t.localScale = new Vector3(-Mathf.Abs(t.localScale.x),
            t.localScale.y, t.localScale.z); 
        }
    }
// Jumping
    if (Input.GetKeyDown(KeyCode.W) && isGrounded)
    {
        r2d.linearVelocity = new Vector2(r2d.linearVelocity.x, jumpHeight);
    }
    // Camera follow
    if (mainCamera)
    {
        mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
    }
}
void FixedUpdate()
{
    Bounds colliderBounds = mainCollider.bounds;
    float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
    Vector3 groundCheckPos = colliderBounds.min + new
    Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
    // Check if player is grounded
    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
    //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
    isGrounded = false;
    if (colliders.Length > 0)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != mainCollider)
            {
               isGrounded = true;
                break;
            }
        }
    }
    // Apply movement velocity
    r2d.linearVelocity = new Vector2((moveDirection) * maxSpeed, r2d.linearVelocity.y);
    // Simple debug
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object hitting the player is an enemy projectile
        if (collision.CompareTag("EnemyBullet"))
        {
            // Calculate the push direction (from projectile to player)
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Apply the movement force
            StartCoroutine(ApplyKnockback(pushDirection));

            // Optional: Destroy the projectile after impact
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        isBeingKnockedback = true;

        // Clear existing velocity to make the push feel sharp and responsive
        rb.linearVelocity = Vector2.zero;

        // Apply a sudden impulse force
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // Wait for the duration of the push
        yield return new WaitForSeconds(knockbackDuration);

        isBeingKnockedback = false;
    }

}
