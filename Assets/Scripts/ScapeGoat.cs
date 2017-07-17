using UnityEngine;

public class ScapeGoat : MonoBehaviour {

    private Player player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            player.PickUpScapeGoat();
            Destroy(this.gameObject);
        }
    }
}
