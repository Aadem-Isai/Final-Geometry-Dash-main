using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      
    public float smoothSpeed = 2f;  // REDUCED from 5 to 2 (moves less/slower)
    public float minY = 0f;   
    public Vector3 offset = new Vector3(0, 1, -10); 

    private bool followEnabled = true;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        // Zoom out by increasing orthographic size
        if (cam != null && cam.orthographic)
        {
            cam.orthographicSize = 8f;  // Default is usually 5, increase to zoom out
        }
    }

    void LateUpdate()
    {
        if (target == null || !followEnabled) return;

        Vector3 desiredPosition = target.position + offset;

        desiredPosition.y = Mathf.Max(desiredPosition.y, minY);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void StopFollowing()
    {
        followEnabled = false;
    }
}
