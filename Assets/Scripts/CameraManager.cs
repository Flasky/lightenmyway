using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public bool ShouldFollowPlayer;
    public GameObject Joystick;
    private Player player;
    private Vector3 startPosition;
    private LevelController levelController;
    private GameObject start;
    private GameObject end;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        levelController = GameObject.Find("LevelController").gameObject.GetComponent<LevelController>();
        start = GameObject.Find("Start").gameObject;
        end = GameObject.Find("End").gameObject;
        ShouldFollowPlayer = true;

        if (levelController.levelNo != 0) {
            startPosition = start.transform.position;
            player.transform.position = startPosition + new Vector3(-4.53f, 0.4f, 0f);
            player.receiveInput = false;
            player.GetComponent<CapsuleCollider2D>().isTrigger = true;
            Joystick.SetActive(false);

            StartCoroutine(PlayerToStartCoroutine());
        }
    }

    void Update() {
        if (ShouldFollowPlayer) {
            transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        }
    }

    IEnumerator PlayerToStartCoroutine() {
        while ((player.transform.position.x - startPosition.x) < 0f) {
            player.rb.velocity = new Vector2(player.maxSpeed, 0f);
            yield return new WaitForSeconds(Time.deltaTime/2);
        }
        player.rb.velocity = Vector2.zero;
        StartCoroutine(MoveToEndCoroutine());
    }

    IEnumerator MoveToEndCoroutine() {
        ShouldFollowPlayer = false;

        float cameraFrameTime = Time.deltaTime;
        float cameraMoveSpeed = 20f;
        Vector2 direction;

        yield return new WaitForSeconds(0.3f);

        // move to the end
        direction = (end.transform.position - start.transform.position).normalized;

        while((transform.position.x - end.transform.position.x) < 0f) {
            transform.Translate(direction * cameraMoveSpeed * cameraFrameTime);
            yield return new WaitForSeconds(cameraFrameTime);
        }

        // stay at the right most point, for 3 seconds
        yield return new WaitForSeconds(1f);

        // move to the left
        direction = (start.transform.position - end.transform.position).normalized;
        while((transform.position.x - start.transform.position.x) > 0f) {
            transform.Translate(direction * cameraMoveSpeed * cameraFrameTime);
            yield return new WaitForSeconds(cameraFrameTime);
        }

        EndAnimation();

    }

    private void EndAnimation() {
        player.receiveInput = true;
        player.GetComponent<CapsuleCollider2D>().isTrigger = false;
        Joystick.SetActive(true);
        ShouldFollowPlayer = true;
    }

	public void Shake() {
        Debug.Log("Camera Shaked");
    }
}
