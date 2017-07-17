using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	public bool HasBeenPicked = false;
    public float HoldingTime = 2f;
    public Sprite PickedUpSprite;
    public GameObject BigLight;
    public GameObject SmallLight;

    public GameObject Empty;
    public GameObject Filled;

    [SerializeField]
    private float timePressed = 0f;
    private Player player;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        BigLight.SetActive(true);
        SmallLight.SetActive(false);
    }

    void Update() {
        if (!HasBeenPicked && timePressed == 0) {
            Empty.SetActive(false);
        } else if (!HasBeenPicked && timePressed > 0f) {
            Empty.SetActive(true);
            Filled.transform.localScale = new Vector3(timePressed/2f, 1f, 1f);
        }
    }

    public void Press() {
        if (!HasBeenPicked) {
            timePressed += Time.deltaTime;
            if (timePressed >= 2f) {
                PickUp();
            }
        }
    }

    void PickUp() {
        HasBeenPicked = true;
        player.PickUpFlower(1);
        GetComponent<SpriteRenderer>().sprite = PickedUpSprite;
        BigLight.SetActive(false);
        SmallLight.SetActive(true);
        Empty.SetActive(false);
    }
}
