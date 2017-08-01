using System.Collections;
using UnityEngine;

public class Crack : MonoBehaviour {

    public Sprite[] crackSprites;
    public int stage;
    public float timeLimit;
    public GameObject stage2Light;
    public GameObject stage3Light;

    [SerializeField]
    private float timeStoodThisStage;
    private bool hasShakenThisStage = false;
    private SpriteRenderer spriteRenderer;
    private CameraManager cameraManager;
    private Player player;
    private bool isPlayerHere = false;
    private bool justChangedStage = false;

    void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = crackSprites[stage - 1];
        ChangeLight();

        cameraManager = Camera.main.GetComponent<CameraManager>();

        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    void Update() {
        if (isPlayerHere) {
            timeStoodThisStage += Time.deltaTime;

            if (timeStoodThisStage >= 1f && !hasShakenThisStage) {
                hasShakenThisStage = true;
            }

            if (timeStoodThisStage >= timeLimit) {
                EnterNextStage();
            }
        }
    }

    void OnTriggerEnter2D (Collider2D colliderD) {
        if (colliderD.gameObject.tag == "Player") {
            isPlayerHere = true;

            if (stage == 3) {
                player.Die();
            }
        }
    }

    void OnTriggerExit2D (Collider2D colliderD) {
        if (colliderD.gameObject.tag == "Player") {
            EnterNextStage();
            isPlayerHere = false;
        }
    }

    public void EnterNextStage() {
        if (!justChangedStage) {
            justChangedStage = true;
            StartCoroutine(ResetChangeStageBool());
            if (stage < 3) {
                stage += 1;
                spriteRenderer.sprite = crackSprites[stage - 1];
                hasShakenThisStage = false;
                timeStoodThisStage = 0f;
                ChangeLight();
                cameraManager.Shake(1);
                // when the stage becomes 3
                if (stage == 3) {
                    if ((player.transform.position - this.transform.position).magnitude < 0.4f) {
                        player.Die();
                    }
                }
            }

            if (stage == 2) {
                //TODO: shake visual effect and crack sound effect
            }
        }
    }

    IEnumerator ResetChangeStageBool() {
        yield return new WaitForSeconds(0.5f);
        justChangedStage = false;
    }

    private void ChangeLight() {
        switch (stage) {
            case 1:
                stage2Light.SetActive(false);
                stage3Light.SetActive(false);
                break;
            case 2:
                stage2Light.SetActive(true);
                stage3Light.SetActive(false);
                break;
            case 3:
                stage2Light.SetActive(false);
                stage3Light.SetActive(true);
                break;
            default:
                Debug.LogError("Crack error in Change Light");
                break;
        }
    }
}
