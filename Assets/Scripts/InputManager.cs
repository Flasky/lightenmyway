using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    public GameObject movingLightPrefab;
    public GameObject movingLight;
    public GameObject lightOnGround;
    public int trackedTouchID;

	private Player player;
    private bool trackingTouch = false;
    private Vector3 originalPosition;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    void Update() {
        if (Input.touchCount > 0 && !trackingTouch) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began) {

                    List<RaycastResult> results = new List<RaycastResult>();
                    PointerEventData pointerData = new PointerEventData(EventSystem.current);
                    pointerData.position = Input.GetTouch(i).position;
                    EventSystem.current.RaycastAll(pointerData, results);

                    foreach (RaycastResult raycastResult in results){
                        Debug.Log(raycastResult.gameObject.name);
                        if (raycastResult.gameObject.name == "Crystal") {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            Vector3 newPosition = new Vector3 (
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingLight = Instantiate(movingLightPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;
                        }
                    }
                }
            }
        } else {
            for (int i = 0; i < Input.touchCount; i++) {
                if (Input.GetTouch(i).fingerId == trackedTouchID) {
                    if (Input.GetTouch(i).phase == TouchPhase.Moved) {
                        Vector3 newPosition = new Vector3 (
                                Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                0f);
                        movingLight.transform.position = newPosition;
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Ground", "Tile"));

                        if (hit.collider != null) {
                            if (player.HasLightShard()) {
                                Instantiate(lightOnGround, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion());
                                Destroy(movingLight);
                                player.UseLightShard();
                                trackingTouch = false;
                            }
                        } else { // go back
                            StartCoroutine(GoBackCoroutine());
                        }
                    }
                }
            }

        }
    }

    private IEnumerator GoBackCoroutine() {
        while ((movingLight.transform.position - originalPosition).magnitude > 0.1f) {
            Vector3 movingDirection = (originalPosition - movingLight.transform.position).normalized;
            float moveSpeed = 1f;
            movingLight.transform.Translate(movingDirection * moveSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(movingLight);
        trackingTouch = false;
    }
}
