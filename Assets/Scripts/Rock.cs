using System.IO;
using UnityEngine;

public class Rock : MonoBehaviour {

    public GameObject scapeGoat;
    public GameObject lightShard1;
    public GameObject lightShard2;
    public GameObject lightShard3;
    public Sprite[] sprites;
    public GameObject Filled;
    public GameObject Empty;


    public bool ShouldSpawnScapegoat;
    [SerializeField]
    private int timeHit;
    private Player player;
    private SpriteRenderer spriteRenderer;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeHit = 0;
        Empty.SetActive(false);
    }

    void LateUpdate() {

		switch (timeHit) {
            case 0:
                spriteRenderer.sprite = sprites[0];
                break;
			case 1:
                spriteRenderer.sprite = sprites[1];
                Filled.transform.localScale = new Vector3(0.33f, 1f, 1f);
                Filled.transform.localPosition = new Vector3(-0.87f, 0f, 0f);
				break;
			case 2:
                spriteRenderer.sprite = sprites[2];
                Filled.transform.localScale = new Vector3(0.67f, 1f, 1f);
                Filled.transform.localPosition = new Vector3(-0.43f, 0f, 0f);
				break;
			default:
				break;
		}

	}

    public void GetHit() {
        timeHit++;
        if (timeHit == 1) {
            Empty.SetActive(true);
        }
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
