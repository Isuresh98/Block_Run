using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
    public float smoothTime = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }
    void LateUpdate()
    {
   
        // Check if the player was found
         if (_player != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = _player.transform.position + offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
            

    }
}
