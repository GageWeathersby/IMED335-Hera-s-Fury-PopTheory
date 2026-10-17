using UnityEngine;
using UnityEngine.UIElements;

public class AimCrossbow : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        // 1. Get the mouse position in screen coordinates and convert to world coordinates
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // set z-axis to 0 because it's 2D

        // 2. Calculate the direction vector from the gun to the mouse
        Vector3 direction = mousePos - transform.position;

        // 3. Calculate the angle in radians, then convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply rotation around the Z-axis
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); //grabs info from the crossbow's sprite renderer

        // "angle" is the calculated angle from Mathf.Atan2
        if (angle > 90f || angle < -90f)
        {
            spriteRenderer.flipY = true; // Flips the gun so it's not upside down
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}