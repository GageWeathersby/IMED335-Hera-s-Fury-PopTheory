using UnityEngine;
public class Bullet: MonoBehaviour
{
    public float speed = 20f;

    public int damage = 10;

    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Automatically push the bullet forward along on the right axis
        rb.linearVelocity = transform.right * speed;

        // Destroys the bullet aftet 3 secs to prevent lag
        Destroy(gameObject, 3f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Pass bullet damage configuration value
            }
            Destroy(gameObject); // Destroy the bullet
        }
    }


 

    // Update is called once per frame
    void Update()
    {
        
    }
}
