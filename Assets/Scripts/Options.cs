using System.Xml.Serialization;
using UnityEngine;

public class Options : MonoBehaviour {

    public float SFXVolume;
    public float MusicVolume;
    public AudioSource[] audioSources;

    public GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SFXVolume = 10f;
        MusicVolume = 10f;
    }

    public float IncreaseSFXVolume() {
        SFXVolume += 1f;
        if (SFXVolume > 10f) SFXVolume = 10f;
        UpdateSFXVolume();
        return SFXVolume;
    }

    public float DecreaseSFXVolume() {
        SFXVolume -= 1f;
        if (SFXVolume < 0f) SFXVolume = 0f;
        UpdateSFXVolume();
        return SFXVolume;
    }

    public void UpdateSFXVolume() {
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources) {
            if (audioSource.gameObject.name != "GameManager") {
                audioSource.volume = SFXVolume/10f;
            }
        }
    }

    public float IncreaseMusicVolume() {
        MusicVolume += 1f;
        if (MusicVolume > 10f) MusicVolume = 10f;
        UpdateMusicVolume();
        return MusicVolume;
    }

    public float DecreaseMusicVolume() {
        MusicVolume -= 1f;
        if (MusicVolume < 0f) MusicVolume = 0f;
        UpdateMusicVolume();
        return MusicVolume;
    }

    public void UpdateMusicVolume() {
        gameManager.GetComponent<AudioSource>().volume = MusicVolume/10f;

    }

    public void UpdateLanguage(Language.LanguageEnum languageEnum) {
        gameManager.UpdateLanguage(languageEnum);
    }
}
