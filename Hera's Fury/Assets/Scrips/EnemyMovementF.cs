using UnityEngine;

public class EnemyMovementF : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f; // speed of the enemy 

    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Find the player object using the tag 
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
    }
    private void FixedUpdate()
    {
         //Physics calclations and movement should always happen inside FixedUpdate 
         if (playerTransform != null)
        {
            // Calculate the direction vector form the enemy to the player 
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            //move the rigidbody towards the player position 
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
