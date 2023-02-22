using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
    public float smoothTime = 0.3f;
    public Vector3 offset =new Vector3(0f, 0f, -10f);

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
            Vector3 desiredPosition = _player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
            transform.position = smoothedPosition;
        }


    }
}
