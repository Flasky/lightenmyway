using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }

	public void StartGame() {
        SceneManager.LoadScene("Level0");
    }
}
