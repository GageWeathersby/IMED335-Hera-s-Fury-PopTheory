using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform startPoint;
    public Transform endPoint;

    private Vector3 targetPosition;

    void Start()
    {
        // Set the starting position and initial target
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    void Update()
    {
        // Move frame-rate independently towards the active target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Switch targets when the enemy gets close enough to its current target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (targetPosition == endPoint.position)
            {
                targetPosition = startPoint.position;
                FlipSprite(true); // Flip to face left
            }
            else
            {
                targetPosition = endPoint.position;
                FlipSprite(false); // Flip to face right
            }
        }
    }

    void FlipSprite(bool faceLeft)
    {
        // Adjusts the local scale to visually flip the enemy sprite
        Vector3 newScale = transform.localScale;
        newScale.x = faceLeft ? -Mathf.Abs(newScale.x) : Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }
}