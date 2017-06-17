using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(0f, 10f)]
    public float maxSpeed;

    public float maxSanity;
    public float sanity;
    [Tooltip("How many sanity lost per second")]
    public float sanityDropSpeed;

    public bool isLit = false;

    private Rigidbody2D rb;
    private LevelController levelController;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        sanity = maxSanity;

        transform.position = GameObject.Find("Start").gameObject.transform.position + new Vector3(0f, 0.4f, 0f);
	}
	
	void Update() {
        if (!isLit) {
            sanity -= sanityDropSpeed * Time.deltaTime;
        }
    }

	void FixedUpdate () {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * maxSpeed);
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
        isLit = false;
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "Light") {
            isLit = true;
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.tag == "Light") {
            isLit = false;
        }
    }

    public float GetSanityInPercentage() {
        return sanity / maxSanity;
    }

    public void PickUpLightShard (int count) {
        levelController.AddLightShard(count);
    }

}
