using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	public bool HasBeenPicked = false;
    public float HoldingTime = 2f;
    public GameObject BigLight;
    public GameObject SmallLight;

    public GameObject Empty;
    public GameObject Filled;

    [SerializeField]
    private int timeHit;
    private Player player;

    void Start() {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        BigLight.SetActive(true);
        SmallLight.SetActive(false);
        timeHit = 0;
    }

    void LateUpdate() {
        if (!HasBeenPicked && timeHit == 0) {
            Empty.SetActive(false);
        } else if (!HasBeenPicked && timeHit > 0) {
            Empty.SetActive(true);
            switch (timeHit) {
                case 1:
                    Filled.transform.localScale = new Vector3(0.33f, 1f, 1f);
                    Filled.transform.localPosition = new Vector3(-0.87f, 0f, 0f);
                    break;
                case 2:
                    Filled.transform.localScale = new Vector3(0.67f, 1f, 1f);
                    Filled.transform.localPosition = new Vector3(-0.43f, 0f, 0f);
                    break;
                default:
                    break;
            }
        }
    }

    public void GetHit() {
        if (!HasBeenPicked) {
            timeHit++;
            if (timeHit >= 3) {
                PickUp();
            }
        }
    }

    void PickUp() {
        HasBeenPicked = true;
        player.PickUpFlower(1);
        GetComponent<Animator>().SetTrigger("PickUp");
        BigLight.SetActive(false);
        SmallLight.SetActive(true);
        Empty.SetActive(false);
    }
}
