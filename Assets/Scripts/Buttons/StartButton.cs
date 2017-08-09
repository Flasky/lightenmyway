using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	public void OnClick() {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartGame();
    }
}
