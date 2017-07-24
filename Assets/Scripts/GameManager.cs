using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

	public void StartGame() {
        SceneManager.LoadScene("Level0");
    }
}
