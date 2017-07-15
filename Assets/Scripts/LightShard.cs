using UnityEngine;

public class LightShard : MonoBehaviour {

    public int lightShardCount;
    private Player player;
    private HUDManager hudManager;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
    }

	void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            player.PickUpLightShard(lightShardCount);
            hudManager.PickUpLightShard(lightShardCount, this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
