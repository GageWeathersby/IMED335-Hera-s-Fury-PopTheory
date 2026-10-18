using UnityEngine;

public class Fruit : MonoBehaviour
{
    // This is called automatically by Unity when a physics collision happens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the fruit collided withthe floor, which has the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the fruit
            Destroy(gameObject);
           
        }
    }
}