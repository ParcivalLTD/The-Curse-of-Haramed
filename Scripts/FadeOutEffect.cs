using UnityEngine;

public class FadeOutEffect : MonoBehaviour
{
    public float fadeDuration = 0.5f; // Duration of the fade-out effect in seconds
    public float moveDistance = 1f; // Distance to move the label from bottom to top

    private float initialAlpha; // Initial alpha value of the label
    private float currentAlpha; // Current alpha value of the label
    private float currentFadeTime; // Current fade-out time
    private bool isFading; // Flag to track if the label is currently fading

    private void Start()
    {
        // Initialize the fade-out effect
        InitializeFadeOut();
    }

    private void Update()
    {
        // Check if the label is currently fading
        if (isFading)
        {
            // Update the fade-out effect
            UpdateFadeOut();
        }
    }

    private void InitializeFadeOut()
    {
        // Set the initial alpha value
        initialAlpha = GetComponent<TextMesh>().color.a;

        // Initialize the current alpha value and fade-out time
        currentAlpha = initialAlpha;
        currentFadeTime = 0f;

        // Set the flag to start fading
        isFading = true;
    }

    private void UpdateFadeOut()
    {
        // Update the fade-out time
        currentFadeTime += Time.deltaTime;

        // Calculate the fade-out progress
        float fadeProgress = currentFadeTime / fadeDuration;
        float alpha = Mathf.Lerp(initialAlpha, 0f, fadeProgress);

        // Update the alpha value of the label's color
        Color labelColor = GetComponent<TextMesh>().color;
        labelColor.a = alpha;
        GetComponent<TextMesh>().color = labelColor;

        // Calculate the move distance progress
        float moveProgress = currentFadeTime / fadeDuration;
        float moveDistanceOffset = Mathf.Lerp(0f, moveDistance, moveProgress);

        // Move the label from bottom to top
        transform.localPosition = Vector3.up * moveDistanceOffset;

        // Check if the fade-out effect is complete
        if (currentFadeTime >= fadeDuration)
        {
            // Destroy the label after the fade-out effect
            Destroy(gameObject);
        }
    }
}
