using UnityEngine;
public class Bullet: MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Automatically push the bullet forward along on the right axis
        rb.linearVelocity = transform.right * speed;

        // Destroys the bullet aftet 3 secs to prevent lag
        Destroy(gameObject, 3f);

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Simple hit logic (Can add damage here later)
        if (hitInfo.CompareTag("Enemy"))
        {
            Destroy(hitInfo.gameObject); //Destroys the enemy

            Destroy(gameObject); //Destroys the bullet
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
