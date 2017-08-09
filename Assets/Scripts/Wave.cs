using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour {

    Color originalColor;
    float scale = 1f;
    float growSpeed = 1.5f;
    RectTransform rectTransform;
    Image image;
    float startTime = 0f;
    float duration = 1f;
    Color transparent = new Color (1f, 1f, 1f, 0f);
    bool isInitialized = false;

    public void Initialize(Color color) {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.color = color;
        originalColor = color;
        rectTransform.localScale = Vector3.one * scale;
        startTime = Time.time;
        isInitialized = true;
    }

    void Update() {
        if (isInitialized) {
            scale += growSpeed * Time.deltaTime;
            rectTransform.localScale = Vector3.one * scale;
            image.color = Color.Lerp(originalColor, transparent, (Time.time - startTime) / duration);

            if ((Time.time - startTime) > duration) {
                Destroy(this.gameObject);
            }
        }
    }
}
