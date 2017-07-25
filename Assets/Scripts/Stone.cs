using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public bool DestroyingSelf = false;

	public void DestroyStone() {
        DestroyingSelf = true;
        GetComponent<Animator>().SetTrigger("Destroy");
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
