using UnityEngine;

public class LightShard : MonoBehaviour {

    public int lightShardCount;
    private Player player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

	void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            player.PickUpLightShard(lightShardCount);
            Destroy(this.gameObject);
        }
    }
}
