using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(0f, 10f)]
    public float maxSpeed;

    // sanity
    public float maxSanity;
    public float sanity;
    [Tooltip("How many sanity lost per second")]
    public float sanityDropSpeed;
    public float sanityGainSpeed;
    private float lastFrameSanity;
    public bool IsSanityDropping = false;
    public bool IsSanityIncreasing = false;

    // light
    public Light smallLight;
    public Light largeLight;

    public bool isLit = false;
    public bool receiveInput = true;

    // sound
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private bool playingNormalSound;
    private bool died = false;

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
        audioSource = GetComponent<AudioSource>();

        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        sanity = maxSanity;
        lastFrameSanity = sanity;

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
                if (!died) {
                    Die();
                }
            }
        }

        if (rb.velocity.magnitude > 0.8f * maxSpeed) {
            if (audioSource.clip != audioClips[0]) {
                audioSource.clip = audioClips[0];
                audioSource.loop = true;
                audioSource.Play();
            }
            // playingNormalSound = true;
        } else if (rb.velocity.magnitude < 0.8f * maxSpeed && rb.velocity.magnitude > 0f){
            if (audioSource.clip != audioClips[1]) {
                audioSource.clip = audioClips[1];
                audioSource.loop = true;
                audioSource.Play();
                // playingNormalSound = false;
            }
        } else if (rb.velocity == Vector2.zero && !died){
            audioSource.Stop();
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

    void LateUpdate() {
        if (sanity < lastFrameSanity) {
            if (!IsSanityDropping) {
                IsSanityDropping = true;
                StopCoroutine(SanityIncreaseCoroutine());
                StartCoroutine(SanityDropCoroutine());
            }
        } else {
            IsSanityDropping = false;
        }

        if (sanity > lastFrameSanity) {
            // only do this when the sanity change from dropping to increasing
            if (!IsSanityIncreasing) {
                IsSanityIncreasing = true;
                StopCoroutine(SanityDropCoroutine());
                StartCoroutine(SanityIncreaseCoroutine());
            }
        } else {
            IsSanityIncreasing = false;
        }

        lastFrameSanity = sanity;
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "LightShard") {
            StartCoroutine(LightsOutCoroutine());
        }
        if (collider.gameObject.tag == "End") {
            levelController.Win();
        }
    }

    IEnumerator SanityIncreaseCoroutine() {
        float stepTime = 0.015f;
        float duration = 0.5f;
        Color cyan = new Color(200f/255f, 1f, 1f, 1f);
        for (float time = 0f; time < duration; time += stepTime) {
            smallLight.color = Color.Lerp(Color.white, cyan, time/duration);
            largeLight.color = Color.Lerp(Color.white, cyan, time/duration);
            yield return new WaitForSeconds(stepTime);
        }

        float normalSpotAngle = 42f;
        float smallSpotAngle = 32f;
        float angleChangeSpeed = 6.67f;
        bool angleDecreasing = true;

        while (true) {
            Debug.Log("Sanity increase coroutine");
            if (angleDecreasing) {
                if (smallLight.spotAngle > smallSpotAngle) {
                    smallLight.spotAngle -= angleChangeSpeed * stepTime;
                    yield return new WaitForSeconds(stepTime);
                } else {
                    angleDecreasing = false;
                    yield return new WaitForSeconds(stepTime);
                }
            } else if (!angleDecreasing) {
                // angle increasing
                if (smallLight.spotAngle < normalSpotAngle) {
                    smallLight.spotAngle += angleChangeSpeed * stepTime;
                    yield return new WaitForSeconds(stepTime);
                } else {
                    angleDecreasing = true;
                    yield return new WaitForSeconds(stepTime);
                }
            }

            if (IsSanityDropping) {
                break;
            }
        }

    }

    IEnumerator SanityDropCoroutine() {
        float stepTime = 0.015f;
        float duration = 0.5f;
        Color cyan = new Color(200f/255f, 1f, 1f, 1f);
        for (float time = 0f; time < duration; time += stepTime) {
            smallLight.color = Color.Lerp(cyan, Color.white, time/duration);
            largeLight.color = Color.Lerp(cyan, Color.white, time/duration);
            smallLight.spotAngle = smallLight.spotAngle + (42f - smallLight.spotAngle) * stepTime;
            yield return new WaitForSeconds(stepTime);
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

    public void Win() {
        receiveInput = false;
        rb.velocity = Vector2.zero;
    }

    public void Die() {
        if (scapeGoat != null) {
            Revive();
        } else {
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine() {
        receiveInput = false;
        rb.velocity = Vector2.zero;
        died = true;
        audioSource.clip = audioClips[2];
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitForSeconds(2f);
        levelController.Lose();
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
