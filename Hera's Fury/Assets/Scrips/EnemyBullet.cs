using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 4f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
   
        Destroy(gameObject, lifetime); // Self-destroy to clean memory leaks
    }

    public void Launch(Vector2 direction)
    {
        // Align arrow rotation smoothly along flight direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect player layers or tags
        if (collision.CompareTag("Player"))
        {
            // Call your Player's damage implementation here, e.g.:
            // collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject, 0.5f);
            //Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Break when hitting solid terrain walls
        }
    }
}