using UnityEngine;

public class RangedEnemyBehavior : MonoBehaviour
{
    [Header("Movement & Detection")]
    public float moveSpeed = 3f;
    public float detectionRange = 8f;
    public float shootingRange = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Ranged Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private float nextFireTime;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find the player automatically via their tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Stop moving and shoot when in optimal ranged distance
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            LookAtPlayer();
            TryShoot();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Chase the player horizontally
            LookAtPlayer();
            ChasePlayer();
        }
        else
        {
            // Idle or simple patrol when out of range
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        float moveDir = direction > 0 ? 1 : -1;

        // Apply physical velocity safely on the X axis, preserving gravity on Y
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);
    }

    void LookAtPlayer()
    {
        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Instantiate enemy arrow at firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            EnemyBullet arrowScript = bullet.GetComponent<EnemyBullet>();

            if (arrowScript != null)
            {
                // Send direction vector to arrow script
                Vector2 shootDir = (player.position - firePoint.position).normalized;
                arrowScript.Launch(shootDir);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f); // Rotate the whole actor to correctly flip graphics & fire points
    }

    private void OnDrawGizmosSelected()
    {
        // Debug circles visualizer in editor scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}