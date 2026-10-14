using UnityEngine;

public class PlayShoot : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Detects when the spacebar or the left mouse is pressed
       if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    void Shoot()
    {
        //Spawn the bullet prefab at the  firepoint pos and rotation 
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
