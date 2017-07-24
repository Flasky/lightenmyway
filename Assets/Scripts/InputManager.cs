using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    public GameObject movingLightPrefab;
    public GameObject movingLight;

    public GameObject movingFlowerPrefab;
    public GameObject movingFlower;

    public GameObject movingScapegoatDollPrefab;
    public GameObject movingScapegoat;

    public GameObject lightOnGround;
    public GameObject flowerOnGround;
    public GameObject scapeGoat;

    public int trackedTouchID;
    public bool trackingTouch = false;
    private Player player;

    // Player movement
    public float Horizontal = 0f;
    public float Vertical = 0f;
    public int movementTouchID;
    public bool trackingMovement = false;
    private GameObject stick;
    private Vector3 stickOrigin;
    private Vector2 stickAnchoredPosition;
    private float stickMovementRadius;

    private bool placingScapegoat = false;
    private bool placingLight = false;
    private bool placingFlower = false;
    private Vector3 originalPosition;
    private float menuBarPositionX;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        stick = GameObject.Find("Stick").gameObject;
        stickOrigin = stick.transform.localPosition;
        stickMovementRadius = stickOrigin.x;
        stickAnchoredPosition = stick.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        menuBarPositionX = GameObject.Find("Menu Bar").GetComponent<RectTransform>().position.x
            - GameObject.Find("Menu Bar").GetComponent<RectTransform>().rect.width;
    }

    void Update() {
        #region tracking movement
        if (trackingMovement) {
            for (int i = 0; i < Input.touchCount; i++) {
                if (Input.GetTouch(i).fingerId == movementTouchID) {
                    Debug.Log("tracking movement");
                    if (Input.GetTouch(i).phase == TouchPhase.Stationary) {
                        UpdateInputParameters();
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Moved) {
                        stick.transform.localPosition = Input.GetTouch(i).position - stickAnchoredPosition;
                        UpdateInputParameters();
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended) {
                        trackingMovement = false;
                        stick.transform.localPosition = stickOrigin;
                        UpdateInputParameters();
                    }
                }
            }
        }
        #endregion

        #region start tracking movement
        if (Input.touchCount > 0 && !trackingMovement) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began) {

                    List<RaycastResult> results = new List<RaycastResult>();
                    PointerEventData pointerData = new PointerEventData(EventSystem.current);
                    pointerData.position = Input.GetTouch(i).position;
                    EventSystem.current.RaycastAll(pointerData, results);
                    foreach (RaycastResult raycastResult in results) {
                        if (raycastResult.gameObject.name == "Joystick" && !trackingMovement) {
                            movementTouchID = Input.GetTouch(i).fingerId;
                            trackingMovement = true;

                            stick.transform.localPosition = Input.GetTouch(i).position - stickAnchoredPosition;
                            UpdateInputParameters();
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        if (Input.touchCount > 0 && !trackingTouch) {

            // in this loop, find a touch to track
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began) {

                    List<RaycastResult> results = new List<RaycastResult>();
                    PointerEventData pointerData = new PointerEventData(EventSystem.current);
                    pointerData.position = Input.GetTouch(i).position;
                    EventSystem.current.RaycastAll(pointerData, results);

                    #region track item placing and manual UI
                    foreach (RaycastResult raycastResult in results) {

                        if (raycastResult.gameObject.name == "Pause Button") {
                            GameObject.Find("LevelController").GetComponent<LevelController>().Pause();
                        }

                        // start tracking touch
                        // placing crystal
                        if (raycastResult.gameObject.name == "Crystal" && player.HasLightShard()) {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            placingLight = true;
                            Vector3 newPosition = new Vector3(
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingLight = Instantiate(movingLightPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;
                        }

                        // placing scape goat
                        if (raycastResult.gameObject.name == "Scapegoat Doll" && player.HasScapeGoat()) {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            placingScapegoat = true;
                            Vector3 newPosition = new Vector3(
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingScapegoat = Instantiate(movingScapegoatDollPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;
                        }

                        // placing flower
                        if (raycastResult.gameObject.name == "Crystal Flower" && player.HasFlower()) {
                            trackedTouchID = Input.GetTouch(i).fingerId;
                            trackingTouch = true;
                            placingFlower = true;
                            Vector3 newPosition = new Vector3(
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y,
                                    0f);
                            movingFlower = Instantiate(movingFlowerPrefab, newPosition, new Quaternion());
                            originalPosition = newPosition;
                            break;
                        }
                    }
                    #endregion

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

                    // hit flower
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position),
                            Vector2.up, 1000f, LayerMask.GetMask("Items"));

                    if (hit.collider != null && hit.collider.gameObject.name == "Flower") {
                        hit.collider.gameObject.GetComponent<Flower>().Press();
                        break;
                    }
                }
            }
        } else {

            // in this loop, we are tracking a touch
            for (int i = 0; i < Input.touchCount; i++) {
                #region track item placement
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
                        } else if (placingFlower) {
                            movingFlower.transform.position = newPosition;
                        }
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended) {

                        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                        // the design here changed once
                        // the original design is that: these items can only be placed on the ground or on tiles
                        // the new design is: crystal can be placed anywhere
                        if (placingLight) {
                            if (Input.GetTouch(i).position.x < menuBarPositionX) {
                                if (player.HasLightShard()) {
                                    Instantiate(lightOnGround, new Vector3(newPosition.x, newPosition.y, 0f), new Quaternion());
                                    Destroy(movingLight);
                                    player.UseLightShard();
                                    trackingTouch = false;
                                }
                            } else {
                                StartCoroutine(GoBackCoroutine(movingLight));
                                placingLight = false;
                            }

                        } else if (placingFlower) {
                            if (Input.GetTouch(i).position.x < menuBarPositionX) {
                                if (player.HasFlower()) {
                                    Instantiate(flowerOnGround, new Vector3(newPosition.x, newPosition.y, 0f), new Quaternion());
                                    Destroy(movingFlower);
                                    player.UseFlower();
                                    trackingTouch = false;
                                }
                            } else {
                                StartCoroutine(GoBackCoroutine(movingFlower));
                                placingFlower = false;
                            }
                        }

                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f, LayerMask.GetMask("Ground", "Tile"));

                        if (hit.collider != null) {
                            if (placingScapegoat) {
                                if (player.HasScapeGoat()) {
                                    player.SetScapeGoat(Instantiate(scapeGoat, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion()) as GameObject);
                                    Destroy(movingScapegoat);
                                    player.UseScapeGoat();
                                    trackingTouch = false;
                                }
                            }
                        } else { // go back
                            if (placingScapegoat) {
                                StartCoroutine(GoBackCoroutine(movingScapegoat));
                                placingScapegoat = false;
                            }
                        }
                    }
                }
                #endregion
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

    private void UpdateInputParameters() {
        Horizontal = (stick.transform.localPosition.x - stickOrigin.x) / stickMovementRadius;
        Vertical = (stick.transform.localPosition.y - stickOrigin.y) / stickMovementRadius;
    }
}
