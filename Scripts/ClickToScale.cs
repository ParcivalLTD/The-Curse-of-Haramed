using UnityEngine;

public class ClickToScale : MonoBehaviour
{
    public float clickedSizeScale = 0.95f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        transform.localScale = originalScale * clickedSizeScale;
    }

    private void OnMouseUp()
    {
        transform.localScale = originalScale;
    }
}
