using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioClip menuBGM;
    public AudioClip levelBGM;
    public Language.LanguageEnum languageEnum = Language.LanguageEnum.EN;
    public Language language;

    private AudioSource audioSource;

    void Start() {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();
        language = new Language(languageEnum);
    }

	public void StartGame() {
        SceneManager.LoadScene("Select Menu");
    }

    public void LoadLevel(int level) {
        if (level <= 13) {
            if (SceneManager.GetActiveScene().name == "Select Menu"
                || SceneManager.GetActiveScene().name == "Menu") {

                audioSource.Stop();
                SceneManager.LoadScene("Level" + level);
                audioSource.clip = levelBGM;
                audioSource.Play();
            } else {
                SceneManager.LoadScene("Level" + level);
            }
        } else {
            switch (level) {
                case 14:
                    if (SceneManager.GetActiveScene().name.Contains("Level")) {
                        audioSource.Stop();
                        SceneManager.LoadScene("Menu");
                        audioSource.clip = menuBGM;
                        audioSource.Play();
                    } else {
                        SceneManager.LoadScene("Menu");
                    }
                    break;
                case 15:
                    if (SceneManager.GetActiveScene().name.Contains("Level")) {
                        audioSource.Stop();
                        SceneManager.LoadScene("Select Menu");
                        audioSource.clip = menuBGM;
                        audioSource.Play();
                    } else {
                        SceneManager.LoadScene("Select Menu");
                    }
                    break;
                default:
                    break;
            }
        }
    }



}
