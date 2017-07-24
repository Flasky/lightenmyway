using System.Collections;
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

    public GameObject lightOnGround;

    public Rigidbody2D rb;
    private LevelController levelController;
    private HUDManager hudManager;
    private InputManager inputManager;
    private float speed;
    private Animator animator;

    private int lightShardCount;
    private int flowerCount;
    public int MaxFlowerCount = 2;
    private bool hasScapegoatDoll = false;
    private GameObject scapeGoat;

    enum PlayerAnimationState {Standing, WalkingUp, WalkingDown, WalkingSide}
    private PlayerAnimationState playerAnimationState;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        sanity = maxSanity;

        // transform.position = GameObject.Find("Start").gameObject.transform.position + new Vector3(0f, 0.4f, 0f);
        speed = maxSpeed;

        lightShardCount = 0;
        flowerCount = 0;

        hudManager = GameObject.Find("HUD Canvas").GetComponent<HUDManager>();
        animator = GetComponent<Animator>();
        playerAnimationState = PlayerAnimationState.Standing;

        hudManager.UpdateFlowerCountText(flowerCount);
	}

	void Update() {
        if (isLit) {
            sanity += sanityGainSpeed * Time.deltaTime;
            if (sanity > maxSanity) {
                sanity = maxSanity;
            }
        } else {
            sanity -= sanityDropSpeed * Time.deltaTime;
            if (sanity <= 0) {
                Die();
            }
        }

        UpdateAnimationState();

    }

	void FixedUpdate () {
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, rb.velocity.y);
        if (receiveInput) {

            // rb.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * speed, CrossPlatformInputManager.GetAxis("Vertical") * speed);
            rb.velocity = new Vector2(inputManager.Horizontal * speed, inputManager.Vertical * speed);
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

        if (collider.gameObject.tag == "Crack") {
            collider.gameObject.GetComponent<Crack>().EnterNextStage();
        }
    }

    private void UpdateAnimationState() {
        if (rb.velocity == Vector2.zero) {
            if (playerAnimationState != PlayerAnimationState.Standing) {
                animator.SetTrigger("Standing");
                playerAnimationState = PlayerAnimationState.Standing;
            }
            animator.speed = 1f;
        } else if (Vector2.Angle(rb.velocity, Vector2.up) < 45f){
            if (playerAnimationState != PlayerAnimationState.WalkingUp) {
                animator.SetTrigger("WalkingUp");
                playerAnimationState = PlayerAnimationState.WalkingUp;
            }
            animator.speed = rb.velocity.magnitude/maxSpeed;
        } else if (Vector2.Angle(rb.velocity, Vector2.down) < 45f) {
            if (playerAnimationState != PlayerAnimationState.WalkingDown) {
                animator.SetTrigger("WalkingDown");
                playerAnimationState = PlayerAnimationState.WalkingDown;
            }
            animator.speed = rb.velocity.magnitude/maxSpeed;
        } else {
            if (playerAnimationState != PlayerAnimationState.WalkingSide) {
                animator.SetTrigger("WalkingSide");
                playerAnimationState = PlayerAnimationState.WalkingSide;
            }
            animator.speed = rb.velocity.magnitude/maxSpeed;

            if (Vector2.Angle(rb.velocity, Vector2.right) <= 45f) {
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
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
        sanity += 1f;
        hudManager.UpdateLightCountText(lightShardCount);
    }

    public bool HasLightShard() {
        return lightShardCount > 0;
    }

    public void UseLightShard() {
        lightShardCount -= 1;
        hudManager.UpdateLightCountText(lightShardCount);
    }

    public void PickUpFlower(int count) {
        if (flowerCount < 2) {
            flowerCount += count;
        }
        sanity += 1;
        hudManager.UpdateFlowerCountText(flowerCount);
    }

    public bool HasFlower() {
        return flowerCount > 0;
    }

    public void UseFlower() {
        flowerCount -= 1;
        hudManager.UpdateFlowerCountText(flowerCount);
    }

    public void PickUpScapeGoat() {
        hasScapegoatDoll = true;
    }

    public bool HasScapeGoat() {
        return hasScapegoatDoll;
    }

    public void UseScapeGoat() {
        hasScapegoatDoll = false;
    }

    public void SetScapeGoat(GameObject scapeGoat) {
        this.scapeGoat = scapeGoat;
    }

    public void Die() {
        if (scapeGoat != null) {
            Revive();
        } else {
            levelController.Lose();
        }

    }

    /*
        Character revives.
        A light will be placed on the ground automatically without consuming any crystal or flower.
        If player has less than 3 crystals, player can get 2 crystals.
        If player has more than 2 crystals, the amount of crystals won’t change.
     */

    private void Revive() {
        this.transform.position = scapeGoat.transform.position;
        Instantiate(lightOnGround, this.transform.position, new Quaternion());

        if (lightShardCount < 3) {
            PickUpLightShard(2);
        }

        Destroy(scapeGoat);
        scapeGoat = null;
    }
}
