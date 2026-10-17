using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform startPoint;
    public Transform endPoint;

    private Vector3 targetPosition;
    private Rigidbody2D rb;

    void Start()
    {
        // Set the starting position and initial target
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    void FixedUpdate()
    {
        float directionX = Mathf.Sign(targetPosition.x - transform.position.x);
        //Moves the enemy 
        rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);

        // check if past target 
        if ((directionX > 0 && transform.position.x >= targetPosition.x) ||
          (directionX < 0 && transform.position.x <= targetPosition.x))
        {
            SwitchTarget();
        }
    }
    void SwitchTarget()
    {
        if (targetPosition == endPoint.position)
        {
            targetPosition = startPoint.position;

        }
        else
        {
            targetPosition = endPoint.position;
         }
    }
    void Update()
    {

    }

    void FlipSprite(bool faceLeft)
    {
        // Adjusts the local scale to visually flip the enemy sprite
        Vector3 newScale = transform.localScale;
        newScale.x = faceLeft ? -Mathf.Abs(newScale.x) : Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }
}
