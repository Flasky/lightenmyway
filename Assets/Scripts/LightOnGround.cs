using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnGround : MonoBehaviour {

    private Light smallLight;
    private Light largeLight;


    void Start () {
		smallLight = this.gameObject.transform.GetChild(0).Find("Smaller").gameObject.GetComponent<Light>();
        largeLight = this.gameObject.transform.GetChild(0).Find("Larger").gameObject.GetComponent<Light>();
	    StartCoroutine(DieOutCoroutine());
    }
	
    IEnumerator DieOutCoroutine() {
        float stepTime = 0.015f;
        float duration = 5f;
        for (float time = 0f; time < duration; time += stepTime) {
            smallLight.intensity = smallLight.intensity - (smallLight.intensity - 0f) * time/200f;
            largeLight.intensity = largeLight.intensity - (largeLight.intensity - 0f) * time/200f;
            yield return new WaitForSeconds(stepTime);
        }
        Destroy(this.gameObject);
    }
}
