using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingLight : MonoBehaviour {

    private Light light;
    private float originalIntensity;

	void Start () {
        light = GetComponent<Light>();
        originalIntensity = light.intensity;
        StartCoroutine(BreatheCoroutine());
	}

    IEnumerator BreatheCoroutine() {
        float startTime = Time.time;
        float duration = 6f;
		float timeElapsed;
        float pieceElapsed;

        while (true) {
            while ((Time.time - startTime) <= duration) {
                timeElapsed = Time.time - startTime;
                if (timeElapsed < 3f) {
                    light.intensity = originalIntensity;
                } else if (timeElapsed < 3.5f) {
                    pieceElapsed = timeElapsed - 3.0f;
                    light.intensity = originalIntensity * (1f - pieceElapsed / 0.5f);
                } else if (timeElapsed < 4f) {
                    pieceElapsed = timeElapsed - 3.5f;
                    light.intensity = originalIntensity * (pieceElapsed / 0.5f);
                } else if (timeElapsed < 4.5f) {
                    pieceElapsed = timeElapsed - 4.0f;
                    light.intensity = originalIntensity * (1f - pieceElapsed / 0.5f);
                } else if (timeElapsed < 5f) {
                    pieceElapsed = timeElapsed - 4.5f;
                    light.intensity = originalIntensity * (pieceElapsed / 0.5f);
                } else if (timeElapsed < 5.5f) {
                    pieceElapsed = timeElapsed - 5.0f;
                    light.intensity = originalIntensity * (1f - pieceElapsed / 0.5f);
                } else if (timeElapsed < 6.0f) {
                    pieceElapsed = timeElapsed - 5.5f;
                    light.intensity = originalIntensity * (pieceElapsed / 0.5f);
                }

                yield return new WaitForSeconds(0.02f);
            }

            startTime = Time.time;
        }
    }
}
