using UnityEngine;

public class CanvasPositionSaver : MonoBehaviour
{
    private static Vector3 savedPosition;

    private void Awake()
    {
        // Set the position of the Canvas to the saved position.
        transform.position = savedPosition;
    }

    private void OnDisable()
    {
        // Save the current position of the Canvas.
        savedPosition = transform.position;
    }
}
