using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlocker : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
    private bool isBeingDisplayed = true;
    private Color transparent = new Color(1f, 1f, 1f, 0f);
    private Color black = Color.black;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D colliderD) {
        Debug.Log("Light blocker encountered something, the tag is: " + colliderD.gameObject.tag);
        if (colliderD.gameObject.tag == "Light" || colliderD.gameObject.tag == "Cold Light") {
            Debug.Log("Light blocker encountered light");
            if (isBeingDisplayed) {
                spriteRenderer.color = transparent;
                isBeingDisplayed = false;
                Debug.Log("Light blocker color set to transparent");
            }
        }
    }

    void OnTriggerStay2D(Collider2D colliderD) {
        if (colliderD.gameObject.tag == "Light" || colliderD.gameObject.tag == "Cold Light") {
            if (isBeingDisplayed) {
                spriteRenderer.color = transparent;
                isBeingDisplayed = false;
                Debug.Log("Light blocker color set to transparent");
            }
        }
    }

    void OnTriggerExit2D(Collider2D colliderD) {
        if (colliderD.gameObject.tag == "Light" || colliderD.gameObject.tag == "Cold Light") {
            if (!isBeingDisplayed) {
                spriteRenderer.color = black;
                isBeingDisplayed = true;
                Debug.Log("Light blocker color set to black");
            }
        }
    }

}
