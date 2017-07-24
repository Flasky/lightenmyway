using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger3 : MonoBehaviour {

    private TutManager tutManager;
    public GameObject zoomImage;
    private bool triggered = false;

    void Start() {
        tutManager = GameObject.Find("TutManager").GetComponent<TutManager>();
        zoomImage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            if (!triggered) {
                StartCoroutine(ShowZoomAnimationCoroutine());
                triggered = true;
            }
        }
    }

    IEnumerator ShowZoomAnimationCoroutine() {
        zoomImage.SetActive(true);
        yield return new WaitForSeconds(6f);
        zoomImage.SetActive(false);
    }
}
