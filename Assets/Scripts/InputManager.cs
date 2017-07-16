using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    public GameObject movingLightPrefab;
    public GameObject movingLight;

    public GameObject movingScapegoatDollPrefab;
    public GameObject movingScapegoat;

    public GameObject lightOnGround;
    public GameObject scapeGoat;
    public int trackedTouchID;

	private Player player;
    public bool trackingTouch = false;
    private bool placingScapegoat = false;
    private bool placingLight = false;
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
                        // start tracking touch
                        if (raycastResult.gameObject.name == "Crystal") {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            placingLight = true;
                            Vector3 newPosition = new Vector3 (
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingLight = Instantiate(movingLightPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;

                        }

                        if (raycastResult.gameObject.name == "Scapegoat Doll") {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            placingScapegoat = true;
                            Vector3 newPosition = new Vector3 (
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingScapegoat = Instantiate(movingScapegoatDollPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;

                        }
                    }

                    // hit road block
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position),
                            Vector2.up, 1000f, LayerMask.GetMask("Tile"));

                    if (hit.collider != null && hit.collider.gameObject.name == "Crystal Stone") {
                        if (player.HasLightShard()) {
                            hit.collider.gameObject.GetComponent<CrystalStone>().DestroyCrystalStone();
                            player.UseLightShard();
                        }
                        break;
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
                        if (placingLight) {
                            movingLight.transform.position = newPosition;
                        } else if (placingScapegoat) {
                            movingScapegoat.transform.position = newPosition;
                        }
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Ground", "Tile"));

                        if (hit.collider != null) {
                            if (placingLight) {
                                if (player.HasLightShard()) {
                                    Instantiate(lightOnGround, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion());
                                    Destroy(movingLight);
                                    player.UseLightShard();
                                    trackingTouch = false;
                                }
                            } else if (placingScapegoat) {
                                if (player.HasScapeGoat()) {
                                    player.SetScapeGoat(Instantiate(scapeGoat, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion()) as GameObject);
                                    Destroy(movingScapegoat);
                                    player.UseScapeGoat();
                                    trackingTouch = false;
                                }
                            }
                        } else { // go back
                            if (placingLight) {
                                StartCoroutine(GoBackCoroutine(movingLight));
                                placingLight = false;
                            } else if (placingScapegoat) {
                                StartCoroutine(GoBackCoroutine(movingScapegoat));
                                placingScapegoat = false;
                            }
                        }
                    }
                }
            }

        }
    }

    private IEnumerator GoBackCoroutine(GameObject goBackObject) {
        while ((goBackObject.transform.position - originalPosition).magnitude > 0.5f) {
            Vector3 movingDirection = (originalPosition - goBackObject.transform.position).normalized;
            float moveSpeed = 1f;
            goBackObject.transform.Translate(movingDirection * moveSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(goBackObject);
        trackingTouch = false;
    }
}
