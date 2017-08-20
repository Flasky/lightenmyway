using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

	public VideoPlayer videoPlayer;

    void Awake() {
        videoPlayer.Play();
        StartCoroutine(CheckStopCoroutine());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S) || Input.touchCount >= 1) {
            SceneManager.LoadScene("Menu");
        }
    }
    IEnumerator CheckStopCoroutine() {
        yield return new WaitForSeconds(50f);
        while (true) {
            if (!videoPlayer.isPlaying) {
                SceneManager.LoadScene("Menu");
            }
            yield return null;
        }
    }



}
