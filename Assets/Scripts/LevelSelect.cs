using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public int levelNo;

    public void LoadLevel() {

        GameObject.Find("GameManager").GetComponent<GameManager>().LoadLevel(levelNo);


    }
}
