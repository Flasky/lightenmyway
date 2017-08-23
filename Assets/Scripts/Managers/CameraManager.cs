using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public bool ShouldFollowPlayer;
    public GameObject Joystick;
    public GameObject SkipText;

    private Player player;
    private Vector3 startPosition;
    private LevelController levelController;
    private GameObject start;
    private GameObject end;
    private bool animating = false;
    private bool animationCanceled = false;
    private GameManager gameManager;

    private GameObject tutObject;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

        if (GameObject.Find("Level Specific Tut") != null) {
            tutObject = GameObject.Find("Level Specific Tut");
            tutObject.SetActive(false);
        }
    }
    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        levelController = GameObject.Find("LevelController").gameObject.GetComponent<LevelController>();
        start = GameObject.Find("Start").gameObject;
        end = GameObject.Find("End").gameObject;
        ShouldFollowPlayer = true;

        if (SkipText != null) {
            SkipText.SetActive(false);
        }

        if (levelController.levelNo != 0) {
            startPosition = start.transform.position;
            player.transform.position = startPosition + new Vector3(-4.53f, 0.4f, 0f);
            player.receiveInput = false;
            player.GetComponent<CapsuleCollider2D>().isTrigger = true;
            Joystick.SetActive(false);
            SkipText.SetActive(true);
            animating = true;

            StartCoroutine(PlayerToStartCoroutine());
        }
    }

    void Update() {
        if (ShouldFollowPlayer) {
            transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        }

        if ((Input.touchCount >= 1 && animating) || Input.GetKeyDown(KeyCode.S)) {
            ManualEndAnimation();
        }
    }

    IEnumerator PlayerToStartCoroutine() {
        while ((player.transform.position.x - startPosition.x) < 0f) {
            player.rb.velocity = new Vector2(player.maxSpeed, 0f);
            player.sanity = player.maxSanity;
            yield return new WaitForSeconds(Time.deltaTime/2);
        }
        player.rb.velocity = Vector2.zero;
        StartCoroutine(MoveToEndCoroutine());
    }

    IEnumerator MoveToEndCoroutine() {
        if (!animationCanceled) {
            ShouldFollowPlayer = false;

            float cameraFrameTime = Time.deltaTime;
            float cameraMoveSpeed = 20f;
            Vector2 direction;

            yield return new WaitForSeconds(0.3f);

            // move to the end
            direction = (end.transform.position - start.transform.position).normalized;

            while ((transform.position.x - end.transform.position.x) < 0f && !animationCanceled) {
                transform.Translate(direction * cameraMoveSpeed * cameraFrameTime);
                yield return new WaitForSeconds(cameraFrameTime);
            }

            // stay at the right most point, for 3 seconds
            yield return new WaitForSeconds(1f);

            // move to the left
            direction = (start.transform.position - end.transform.position).normalized;
            while ((transform.position.x - start.transform.position.x) > 0f && !animationCanceled) {
                transform.Translate(direction * cameraMoveSpeed * cameraFrameTime);
                yield return new WaitForSeconds(cameraFrameTime);
            }

            if (!animationCanceled) {
                EndAnimation();
            }
        }

        yield return new WaitForSeconds(0.2f);


    }

    private void EndAnimation() {
        player.receiveInput = true;
        player.GetComponent<CapsuleCollider2D>().isTrigger = false;
        Joystick.SetActive(true);
        ShouldFollowPlayer = true;
        SkipText.SetActive(false);
        animating = false;

        if (tutObject != null) {
            if (!gameManager.DisplayedLevelTutorials.Contains(levelController.levelNo)) {
                tutObject.SetActive(true);
                Time.timeScale = 0f;
                gameManager.DisplayedLevelTutorials.Add(levelController.levelNo);
            }
        }

    }

    private void ManualEndAnimation() {
        player.transform.position = start.transform.position + new Vector3(0f, 0.4f, 0f);
        StopCoroutine(PlayerToStartCoroutine());
        animationCanceled = true;
        EndAnimation();
    }

	public void Shake(int time) {
        float shakeAngle = 0f; // Random.Range(0f, 360f); // clockwise from up right
        float shakeDistance = 0.05f;
        StartCoroutine(ShakeCoroutine(shakeAngle, shakeDistance, time));
    }

    IEnumerator ShakeCoroutine(float angle, float distance, int time) {
        ShouldFollowPlayer = false;

        float startTime = Time.time;
        Vector3 direction;
        Vector3 displacement;

        // 1st shake
        direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);

        displacement = direction * distance;
        Debug.Log(direction + ", " + displacement);
        transform.position = player.transform.position + displacement - new Vector3(0f, 0f, 10f);
        yield return null;


        startTime = Time.time;
        direction *= -1f;

        for (int i = 0; i < time; i++) {

            displacement = direction * distance;
            transform.position = player.transform.position + displacement - new Vector3(0f, 0f, 10f);
            yield return null;

            startTime = Time.time;
            direction *= -1f;
        }


        displacement = direction * distance;
        transform.position = player.transform.position + displacement - new Vector3(0f, 0f, 10f);
        yield return null;


        ShouldFollowPlayer = true;
    }

    public void ZoomToPlayer(float duration, float targetOrthographicSize) {
        StartCoroutine(ZoomToPlayerCoroutine(duration, targetOrthographicSize));
    }

    IEnumerator ZoomToPlayerCoroutine(float duration, float targetOrthographicSize) {
        float stepTime = 0.015f;
        float originalOrthographicSize = this.gameObject.GetComponent<Camera>().orthographicSize;
        float startTime = Time.time;

        while ((Time.time - startTime) < duration) {
            this.gameObject.GetComponent<Camera>().orthographicSize =
                originalOrthographicSize - (originalOrthographicSize - targetOrthographicSize) * ((Time.time - startTime)/duration); //2f is the smallest size
            yield return new WaitForSeconds(stepTime);
        }

    }
}
