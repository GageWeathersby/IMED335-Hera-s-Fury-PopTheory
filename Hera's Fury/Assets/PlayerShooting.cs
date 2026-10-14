using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet spawns (tip of crossbow)
    public float bulletSpeed = 20f;
    public float fireRate = 0.25f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // 1. Convert mouse position to world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // 2. Calculate direction from firePoint to mouse
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // 3. Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 4. Apply velocity to the bullet's Rigidbody2D
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed; 
        }
    }
}