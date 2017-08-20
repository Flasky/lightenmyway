using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger4 : MonoBehaviour {

    private TutManager tutManager;
    private bool triggered = false;

    void Start() {
        tutManager = GameObject.Find("TutManager").GetComponent<TutManager>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            if (!triggered) {
                tutManager.ShowZoomAnimation();
                triggered = true;
            }
        }
    }
}
