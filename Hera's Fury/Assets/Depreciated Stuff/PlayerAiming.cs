using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    void Update()
    {
        // 1. Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure Z is 0 for 2D

        // 2. Calculate direction from object to mouse
        Vector3 direction = mousePos - transform.position;

        // 3. Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}