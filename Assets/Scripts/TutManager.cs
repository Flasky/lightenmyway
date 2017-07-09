using System.Collections;
using UnityEngine;

public class TutManager : MonoBehaviour{
    private GameObject camera;
    private Player player;
    private LevelController levelController;
    public Transform[] playerPositions;
    public GameObject menuBar;
    public GameObject tutImages;
    public GameObject joystick;
    public GameObject start;
    public GameObject end;
    public GameObject arrow;

    private bool showingImage1 = false;
    private bool showingImage2 = false;
    private bool shouldCameraFollowPlayer = false;

    void Start() {
        camera = Camera.main.gameObject;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.receiveInput = false;
        camera.transform.position = start.transform.position + new Vector3(0f, 0f, -10f);
        player.transform.position = playerPositions[0].position;
        player.GetComponent<CapsuleCollider2D>().isTrigger = true;
        menuBar.SetActive(false);
        joystick.SetActive(false);
        arrow.SetActive(false);
        tutImages.SetActive(false);
        StartCoroutine(PlayerWalkToStartCoroutine());
    }

    void Update () {
        if (shouldCameraFollowPlayer) {
            camera.transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        }
    }

    IEnumerator PlayerWalkToStartCoroutine() {
        Debug.Log((player.transform.position.x - playerPositions[1].position.x));
        while ((player.transform.position.x - playerPositions[1].position.x) < 0f) {
            player.rb.velocity = new Vector2(player.maxSpeed, 0f);
            camera.transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        player.rb.velocity = Vector2.zero;
        StartCoroutine(CameraCoroutine1());
    }

    IEnumerator CameraCoroutine1() {
        yield return new WaitForSeconds(1f);

        while((camera.transform.position.x - end.transform.position.x) < 0f) {
            camera.transform.Translate(Vector2.right * 10f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(3f);

        while((camera.transform.position.x - start.transform.position.x) > 0f) {
            camera.transform.Translate(Vector2.left * 10f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        player.receiveInput = true;
        player.GetComponent<CapsuleCollider2D>().isTrigger = false;
        joystick.SetActive(true);
        shouldCameraFollowPlayer = true;
        StartCoroutine(ArrowCoroutine());
    }

    IEnumerator ArrowCoroutine() {
        arrow.SetActive(true);
        float startTime = Time.time;
        float duration = 5f;

        float animationStartTime = Time.time;
        Vector2 animationDirection = Vector2.right;
        while ((Time.time - startTime) < duration) {
            while ((Time.time - animationStartTime) < 1f) {
                arrow.transform.Translate(animationDirection * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            animationStartTime = Time.time;
            animationDirection *= -1f;
        }
        arrow.SetActive(false);
    }

    public void Trigger1 () {
        menuBar.SetActive(true);
    }

    public void Trigger2 () {
        Time.timeScale = 0.01f;
        menuBar.SetActive(false);
        tutImages.SetActive(true);
        tutImages.transform.FindChild("Image2").gameObject.SetActive(false);
        showingImage1 = true;
    }

    public void CrossButton() {

        if (showingImage1) {
            tutImages.transform.FindChild("Image1").gameObject.SetActive(false);
            tutImages.transform.FindChild("Image2").gameObject.SetActive(true);
            showingImage1 = false;
            showingImage2 = true;
        } else if (showingImage2) {
            tutImages.SetActive(false);
            menuBar.SetActive(true);
            Time.timeScale = 1f;
        }

    }
}
