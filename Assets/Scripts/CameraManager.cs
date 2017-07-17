using UnityEngine;

public class CameraManager : MonoBehaviour {

    public bool ShouldFollowPlayer;
    private Player player;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        ShouldFollowPlayer = true;
    }

    void Update() {
        if (ShouldFollowPlayer) {
            transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
        }
    }

	public void Shake() {
        Debug.Log("Camera Shaked");
    }
}
