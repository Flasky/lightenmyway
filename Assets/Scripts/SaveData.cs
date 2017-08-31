using System;
using System.Collections.Generic;

[Serializable]
public class SaveData {
    public bool ShouldEnterTutorial;
    public int LastPassedLevel;
    public List<int> DisplayedLevelTutorials;
    public Language.LanguageEnum LanguageEnum;
    public float SFXVolume;
    public float MusicVolume;

    public SaveData(bool shouldEnterTutorial, int lastPassedLevel, List<int> displayedLevelTutorials, Language.LanguageEnum languageEnum, float sfxVolume, float musicVolume) {
        ShouldEnterTutorial = shouldEnterTutorial;
        LastPassedLevel = lastPassedLevel;

        DisplayedLevelTutorials = new List<int>();
        foreach (int i in displayedLevelTutorials) {
            DisplayedLevelTutorials.Add(i);
        }

        LanguageEnum = languageEnum;
        SFXVolume = sfxVolume;
        MusicVolume = musicVolume;
    }
}