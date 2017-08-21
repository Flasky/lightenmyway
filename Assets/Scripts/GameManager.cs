using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioClip menuBGM;
    public AudioClip levelBGM;
    public Language.LanguageEnum LanguageEnum;
    public Language language;

    private AudioSource audioSource;

    // data persistence
    public bool ShouldEnterTutorial;
    public int LastPassedLevel;
    public List<int> DisplayedLevelTutorials;

    private Options options;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();

        options = GetComponent<Options>();

        if(HasSaveData()){
            Load();
        } else {
            ShouldEnterTutorial = true;
            LastPassedLevel = -1;
            DisplayedLevelTutorials = new List<int>();
            LanguageEnum = Language.LanguageEnum.EN;
            options.SFXVolume = 10f;
            options.MusicVolume = 10f;
            Save();
        }
        language = new Language(LanguageEnum);
    }

	public void StartGame() {
        if (ShouldEnterTutorial) {
            SceneManager.LoadScene("Level0");
            ShouldEnterTutorial = false;
            Save();
        } else {
            SceneManager.LoadScene("Select Menu");
        }
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

    public void UpdateLanguage(Language.LanguageEnum languageEnum) {
        this.LanguageEnum = languageEnum;
        language.SwitchLanguage(languageEnum);
        UpdateOptions();

        if (GameObject.Find("HUD Canvas") != null) {
            GameObject.Find("HUD Canvas").GetComponent<HUDManager>().UpdateTextLanguage();
        }

        if (GameObject.Find("TutManager") != null) {
            GameObject.Find("TutManager").GetComponent<TutManager>().UpdateTutTextLanguage();
        }

    }


    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);

        SaveData saveData = new SaveData(ShouldEnterTutorial, LastPassedLevel, DisplayedLevelTutorials, LanguageEnum, options.SFXVolume, options.MusicVolume);
        bf.Serialize(file, saveData);
        file.Close();
    }

    public bool HasSaveData() {
        if(File.Exists(Application.persistentDataPath + "/save.dat")) {
            return true;
        } else {
            return false;
        }
    }

    public void Load() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);

        this.ShouldEnterTutorial = save.ShouldEnterTutorial;
        this.LastPassedLevel = save.LastPassedLevel;

        this.DisplayedLevelTutorials = new List<int>();
        foreach (int i in save.DisplayedLevelTutorials) {
            this.DisplayedLevelTutorials.Add(i);
        }
        Debug.Log("Loaded array length: " + this.DisplayedLevelTutorials.Count);
        Debug.Log("Loaded array: " + this.DisplayedLevelTutorials);

        LanguageEnum = save.LanguageEnum;
        options.SFXVolume = save.SFXVolume;
        options.MusicVolume = save.MusicVolume;

        file.Close();
    }

    public void ResetSaveData() {
        if(HasSaveData()) {
            ShouldEnterTutorial = true;
            LastPassedLevel = 0;
            DisplayedLevelTutorials = new List<int>();
            LanguageEnum = Language.LanguageEnum.EN;
            options.SFXVolume = 10f;
            options.MusicVolume = 10f;
            Save();
        }
    }

    public void UpdateSaveData(bool shouldEnterTutorial, int lastPassedLevel, List<int> displayedLevelTutorials) {
        this.ShouldEnterTutorial = shouldEnterTutorial;
        this.LastPassedLevel = lastPassedLevel;
        this.DisplayedLevelTutorials = displayedLevelTutorials;
        Save();
    }

    public void UpdateSaveDate(int lastPassedLevel) {
        this.LastPassedLevel = lastPassedLevel;
        Save();
    }
    public void UpdateOptions() {
        Save();
    }

}
