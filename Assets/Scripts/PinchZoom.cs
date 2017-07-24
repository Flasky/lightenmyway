using System.Collections;
using UnityEngine;

public class PinchZoom : MonoBehaviour {
    public float orthoZoomSpeed = 0.2f;        // The rate of change of the orthographic size in orthographic mode.

    public float maxSize = 11f;
    public float minSize = 3f;

    private Camera camera;
    private InputManager inputManager;
    private bool canZoomNow = false;

    void Start() {
        camera = Camera.main;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update() {
        // If there are more than two touches on the device...

        if (Input.touchCount >= 2)
        {

            Touch touchZero;
            Touch touchOne;

            // not tracking anything else
            if (!inputManager.trackingTouch && !inputManager.trackingMovement) {
                canZoomNow = true;
                touchZero = Input.GetTouch(0);
                touchOne = Input.GetTouch(1);
            }

            // tracking touch but not movement
            if ((inputManager.trackingTouch && !inputManager.trackingMovement)
                || (!inputManager.trackingTouch && inputManager.trackingMovement)){
                Debug.Log(Input.touchCount);
                if (Input.touchCount == 3) {
                    canZoomNow = true;

                    ArrayList availableTouches = new ArrayList();
                    for (int i = 0; i < Input.touchCount; i++) {
                        if (Input.GetTouch(i).fingerId != inputManager.trackedTouchID
                        && Input.GetTouch(i).fingerId != inputManager.movementTouchID) {
                            availableTouches.Add(Input.GetTouch(i));
                        }
                    }
                    touchZero = (Touch) availableTouches[0];
                    touchOne = (Touch) availableTouches[1];

//                    int trackedTouchIndex = 0;
//                    for (int i = 0; i < Input.touchCount; i++) {
//                        if (Input.GetTouch(i).fingerId == inputManager.trackedTouchID
//                            || Input.GetTouch(i).fingerId == inputManager.movementTouchID) {
//                            trackedTouchIndex = i;
//                        }
//                    }
//
//                    switch (trackedTouchIndex) {
//                        case 0:
//                            touchZero = Input.GetTouch(1);
//                            touchOne = Input.GetTouch(2);
//                            break;
//                        case 1:
//                            touchZero = Input.GetTouch(0);
//                            touchOne = Input.GetTouch(2);
//                            break;
//                        case 2:
//                            touchZero = Input.GetTouch(0);
//                            touchOne = Input.GetTouch(1);
//                            break;
//                        default:
//                            break;
//                    }
                }
            }

            if (inputManager.trackingTouch && inputManager.trackingMovement) {
                if (Input.touchCount == 4) {
                    canZoomNow = true;
                    ArrayList availableTouches = new ArrayList();
                    for (int i = 0; i < Input.touchCount; i++) {
                        if (Input.GetTouch(i).fingerId != inputManager.trackedTouchID
                            && Input.GetTouch(i).fingerId != inputManager.movementTouchID) {
                            availableTouches.Add(Input.GetTouch(i));
                        }
                    }
                    touchZero = (Touch) availableTouches[0];
                    touchOne = (Touch) availableTouches[1];
                }
            }

            if (canZoomNow) {
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