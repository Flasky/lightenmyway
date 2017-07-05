using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Range(0f, 10f)]
    public float maxSpeed;

    public float maxSanity;
    public float sanity;
    [Tooltip("How many sanity lost per second")]
    public float sanityDropSpeed;
    public float sanityGainSpeed;

    public bool isLit = false;
    public bool receiveInput = true;

    public Rigidbody2D rb;
    private LevelController levelController;
    private HUDManager hudManager;
    private float speed;

    private int lightShardCount;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        sanity = maxSanity;

        transform.position = GameObject.Find("Start").gameObject.transform.position + new Vector3(0f, 0.4f, 0f);
        speed = maxSpeed;

        lightShardCount = 0;

        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
	}
	
	void Update() {
        if (isLit) {
            sanity += sanityGainSpeed * Time.deltaTime;
            if (sanity > maxSanity) {
                sanity = maxSanity;
            }
        } else {
            sanity -= sanityDropSpeed * Time.deltaTime;
        }
    }

	void FixedUpdate () {
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, rb.velocity.y);
        if (receiveInput) {
            rb.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * speed, CrossPlatformInputManager.GetAxis("Vertical") * speed);
        }
	}

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "LightShard") {
            StartCoroutine(LightsOutCoroutine());
        }
        if (collider.gameObject.tag == "End") {
            levelController.Win();
        }
    }

    // fix a weird bug, when you pick up a light shard but still stay lit
    IEnumerator LightsOutCoroutine() {
        yield return new WaitForSeconds(0.2f);
        LightsOut();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "Light") {
            isLit = true;
            speed = maxSpeed;
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.tag == "Light") {
            LightsOut();
        }
    }

    public void LightsOut() {
        isLit = false;
        speed = maxSpeed / 3f;
    }

    public float GetSanityInPercentage() {
        return sanity / maxSanity;
    }

    public void PickUpLightShard (int count) {
        lightShardCount += count;
        hudManager.UpdateLightCountText(lightShardCount);
    }

    public bool HasLightShard() {
        return lightShardCount > 0;
    }

    public void UseLightShard() {
        lightShardCount -= 1;
        hudManager.UpdateLightCountText(lightShardCount);
    }

}
