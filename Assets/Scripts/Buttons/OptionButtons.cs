using UnityEngine;
using UnityEngine.UI;

public class OptionButtons:MonoBehaviour {

    private Options options;
    public Text SFXVolumeText;
    public Text MusicVolumeText;

    void Start() {
        options = GameObject.Find("GameManager").GetComponent<Options>();
        SFXVolumeText.text = options.SFXVolume.ToString();
        MusicVolumeText.text = options.MusicVolume.ToString();
    }

    void OnEnable() {
        if (SFXVolumeText != null) SFXVolumeText.text = options.SFXVolume.ToString();
        if (MusicVolumeText != null) MusicVolumeText.text = options.MusicVolume.ToString();
    }

    public void SFXLeftButton() {
        SFXVolumeText.text = options.DecreaseSFXVolume().ToString();
    }

    public void SFXRightButton() {
        SFXVolumeText.text = options.IncreaseSFXVolume().ToString();
    }

    public void MusicLeftButton() {
        MusicVolumeText.text = options.DecreaseMusicVolume().ToString();
    }

    public void MusicRightButton() {
        MusicVolumeText.text = options.IncreaseMusicVolume().ToString();
    }

    public void SetLanguageToEN() {
        options.UpdateLanguage(Language.LanguageEnum.EN);
    }

    public void SetLanguageToZHCN() {
        options.UpdateLanguage(Language.LanguageEnum.ZH_CN);
    }

    public void SetLanguageToZHHK() {
        options.UpdateLanguage(Language.LanguageEnum.ZH_HK);
    }
}
