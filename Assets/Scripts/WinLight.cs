using System.Collections;
using UnityEngine;

public class WinLight : MonoBehaviour {
    public float TargetIntensity = 0.8f;
    private Light light;

    public void LightUpWholeMaze() {
        light = GetComponent<Light>();
        StartCoroutine(LightUpWholeMazeCoroutine());
    }

    IEnumerator LightUpWholeMazeCoroutine() {
        float duration = 2f;
        float stepTime = 0.015f;
        float startTime = Time.time;

        while ( (Time.time - startTime) < duration) {
            light.intensity = ( (Time.time - startTime) / duration) * TargetIntensity;
            yield return new WaitForSeconds(stepTime);
        }
    }
}
