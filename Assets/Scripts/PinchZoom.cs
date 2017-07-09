using UnityEngine;

public class PinchZoom : MonoBehaviour {
    public float orthoZoomSpeed = 0.2f;        // The rate of change of the orthographic size in orthographic mode.

    public float maxSize = 11f;
    public float minSize = 3f;

    private Camera camera;
    private InputManager inputManager;

    void Start() {
        camera = Camera.main;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update() {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if ((inputManager.trackingTouch == false)
                || (touchOne.fingerId != inputManager.trackedTouchID && touchZero.fingerId != inputManager.trackedTouchID)) {
                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Min(Mathf.Max(camera.orthographicSize, minSize), maxSize);
            }
        }
    }
}