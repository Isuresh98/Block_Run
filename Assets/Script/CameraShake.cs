using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // The maximum amount the camera can be moved in any direction during the shake
    public float shakeAmount = 0.7f;

    // The duration of the shake effect
    public float shakeDuration = 0.3f;

    // The speed at which the camera returns to its original position after the shake
    public float returnSpeed = 1f;

    // The initial position of the camera before the shake
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Save the original position of the camera
        originalPosition = transform.position;
    }

    // Call this method to start the camera shake effect
    public void ShakeCamera()
    {
        // Start a coroutine to perform the shake effect
        StartCoroutine(Shake());
    }

    // The coroutine that performs the shake effect
    IEnumerator Shake()
    {
        // Get the current time
        float startTime = Time.time;

        // Continue shaking the camera until the duration has elapsed
        while (Time.time - startTime < shakeDuration)
        {
            // Generate a random offset for the camera position
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            Vector3 offset = new Vector3(offsetX, offsetY, 0);

            // Move the camera to the offset position
            transform.position = originalPosition + offset;

            // Wait for a short amount of time before moving the camera back
            yield return new WaitForSeconds(0.01f);
        }

        // Smoothly return the camera to its original position
        while (transform.position != originalPosition)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * returnSpeed);
            yield return null;
        }
    }
}
