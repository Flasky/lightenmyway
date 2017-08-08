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
    public GameObject tutPlace;
    public GameObject zoomImage;

    private CameraManager cameraManager;

    void Start() {
        camera = Camera.main.gameObject;
        cameraManager = camera.GetComponent<CameraManager>();
        camera.transform.position = start.transform.position + new Vector3(0f, 0f, -10f);

        player = GameObject.Find("Player").GetComponent<Player>();
        player.receiveInput = false;
        player.transform.position = playerPositions[0].position;
        player.GetComponent<CapsuleCollider2D>().isTrigger = true;

        menuBar.SetActive(false);
        joystick.SetActive(false);
        arrow.SetActive(false);
        StartCoroutine(PlayerWalkToStartCoroutine());
        cameraManager.ShouldFollowPlayer = true;
    }

    IEnumerator PlayerWalkToStartCoroutine() {

        while ((player.transform.position.x - playerPositions[1].position.x) < 0f) {
            player.rb.velocity = new Vector2(player.maxSpeed, 0f);
            yield return new WaitForSeconds(Time.deltaTime/2);
        }
        player.rb.velocity = Vector2.zero;
        StartCoroutine(CameraCoroutine1());
    }

    IEnumerator CameraCoroutine1() {
        cameraManager.ShouldFollowPlayer = false;
        float cameraFrameTime = Time.deltaTime;
        float cameraMoveSpeed = 20f;

        yield return new WaitForSeconds(1f);

        // move to the right
        while((camera.transform.position.x - end.transform.position.x) < 0f) {
            camera.transform.Translate(Vector2.right * cameraMoveSpeed * cameraFrameTime);
            yield return new WaitForSeconds(cameraFrameTime);
        }

        // stay at the right most point, for 3 seconds
        yield return new WaitForSeconds(3f);

        // move to the left
        while((camera.transform.position.x - start.transform.position.x) > 0f) {
            camera.transform.Translate(Vector2.left * cameraMoveSpeed * cameraFrameTime);
            yield return new WaitForSeconds(cameraFrameTime);
        }

        player.receiveInput = true;
        player.GetComponent<CapsuleCollider2D>().isTrigger = false;
        joystick.SetActive(true);
        cameraManager.ShouldFollowPlayer = true;
        ShowTutImage1();
    }

    public void ShowTutImage1() {
        Time.timeScale = 0.01f;
        menuBar.SetActive(false);
        tutImages.transform.FindChild("Image1").gameObject.SetActive(true);
    }

    public void ShowArrow() {
        Time.timeScale = 1f;
        tutImages.transform.FindChild("Image1").gameObject.SetActive(false);
        StartCoroutine(ArrowCoroutine());
    }

    IEnumerator ArrowCoroutine() {
        arrow.SetActive(true);
        yield return new WaitForSeconds(2.9f);
        arrow.SetActive(false);
    }

    // trigger 1
    public void ShowMenubar() {
        menuBar.SetActive(true);
    }

    // trigger 2
    public void ShowTutImage2() {
        Time.timeScale = 0.01f;
        tutImages.transform.FindChild("Image2").gameObject.SetActive(true);
        menuBar.SetActive(false);
    }

    public void ShowTutImage3() {
        tutImages.transform.FindChild("Image2").gameObject.SetActive(false);
        tutImages.transform.FindChild("Image3").gameObject.SetActive(true);
    }

    public void CloseTutImage3() {
        tutImages.transform.FindChild("Image3").gameObject.SetActive(false);
        Time.timeScale = 1f;
        menuBar.SetActive(true);
    }

    // trigger 3
    public void ShowTutImage4() {
        Time.timeScale = 0.01f;
        tutImages.transform.FindChild("Image4").gameObject.SetActive(true);
        menuBar.SetActive(false);
    }

    public void ShowTutImage5() {
        tutImages.transform.FindChild("Image4").gameObject.SetActive(false);
        tutImages.transform.FindChild("Image5").gameObject.SetActive(true);
    }

    public void CloseTutImage5() {
        tutImages.transform.FindChild("Image5").gameObject.SetActive(false);
        Time.timeScale = 1f;
        menuBar.SetActive(true);
        StartCoroutine(PlaceItemTutCoroutine());
    }

    public void ShowImage6() {
        Time.timeScale = 0.01f;
        tutImages.transform.FindChild("Image6").gameObject.SetActive(true);
        menuBar.SetActive(false);
    }

    public void ShowImage5WhenShowing6() {
        tutImages.transform.FindChild("Image6").gameObject.SetActive(false);
        tutImages.transform.FindChild("Image5").gameObject.SetActive(true);
    }
    IEnumerator PlaceItemTutCoroutine() {
        tutPlace.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutPlace.SetActive(false);

        StartCoroutine(ShowZoomAnimationCoroutine());
    }

    IEnumerator ShowZoomAnimationCoroutine() {
        zoomImage.SetActive(true);
        yield return new WaitForSeconds(6f);
        zoomImage.SetActive(false);
    }
}
