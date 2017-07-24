using UnityEngine;

public class Rock : MonoBehaviour {

    public GameObject scapeGoat;
    public GameObject lightShard1;
    public GameObject lightShard2;
    public GameObject lightShard3;
    public Sprite[] sprites;

    public bool ShouldSpawnScapegoat;
    [SerializeField]
    private int timeHit = 0;
    private Player player;
    private SpriteRenderer spriteRenderer;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {

		switch (timeHit) {
			case 1:
                spriteRenderer.sprite = sprites[1];
				break;
			case 2:
                spriteRenderer.sprite = sprites[2];
				break;
			default:
				break;
		}

	}

    public void GetHit() {
        timeHit++;
        if (timeHit >= 3) {
            Destroy();
        }
    }

    void Destroy() {
        if (ShouldSpawnScapegoat) {
            SpawnScapegoat();
        } else {
            SpawnLightShard();
        }
        Destroy(this.gameObject);
    }

    private void SpawnLightShard() {
        float ran = Random.Range(0f, 1f);
        if (ran < 0.20f) {
            Instantiate(lightShard2, this.transform.position, new Quaternion());
        } else if (ran < 0.80f) {
            Instantiate(lightShard1, this.transform.position, new Quaternion());
        } else {

        }
    }

    private void SpawnScapegoat() {
        Instantiate(scapeGoat, this.transform.position, new Quaternion());
    }
}
