using System.Collections;
using UnityEngine;

// Put this on the last door (its collider must be a trigger).
// When the player walks in, the camera smoothly zooms in on the player
// and the Y offset goes back to 0, so the player is centered.
public class zoom : MonoBehaviour
{
    public Camera Camera;
    public CameraController cameraController;
    public float targetSize = 3f;     // smaller = more zoomed in
    public float duration = 1f;       // how long the zoom takes, in seconds

    private bool done = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done && collision.gameObject.CompareTag("Player"))
        {
            done = true; // only zoom once
            StartCoroutine(ZoomIn());
        }
    }

    private IEnumerator ZoomIn()
    {
        float startSize = Camera.orthographicSize;
        float startOffset = cameraController.yOffset;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            Camera.orthographicSize = Mathf.Lerp(startSize, targetSize, smooth);
            cameraController.yOffset = Mathf.Lerp(startOffset, 0f, smooth);
            yield return null;
        }
    }
}
