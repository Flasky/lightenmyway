using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalStone : MonoBehaviour {

	public void DestroyCrystalStone() {
        GetComponent<Animator>().SetTrigger("Destroy");
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
