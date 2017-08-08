using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

	public VideoPlayer videoPlayer;

    void Awake() {
        GameObject.Find("GameManager").GetComponent<AudioSource>().Pause();
        videoPlayer.Play();
        StartCoroutine(CheckStopCoroutine());
    }

    IEnumerator CheckStopCoroutine() {
        yield return new WaitForSeconds(50f);
        while (true) {
            if (!videoPlayer.isPlaying) {
                GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("Select Menu");
            }
            yield return null;
        }
    }



}
