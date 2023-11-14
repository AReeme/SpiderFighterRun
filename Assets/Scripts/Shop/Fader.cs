using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Initialize the CanvasGroup alpha to 0 (fully transparent)
        canvasGroup.alpha = 0f;
    }

    public void StartFadeIn()
    {
        // Ensure the CanvasGroup is active
        canvasGroup.gameObject.SetActive(true);

        // Start the fade-in animation
        StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the new alpha value using Lerp
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // Update the CanvasGroup's alpha
            canvasGroup.alpha = newAlpha;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure that alpha is fully 1 (opaque)
        canvasGroup.alpha = 1f;
    }
}