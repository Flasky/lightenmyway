using UnityEngine;

public class LevelMenuManager : MonoBehaviour{
    public GameObject[] LevelMaps;
    public GameObject[] MapsFragments;
    public GameObject[] Levels;
    public GameObject[] LevelConnects;

    void Start() {
        int lastPassedLevel = GameObject.Find("GameManager").GetComponent<GameManager>().LastPassedLevel;
        InitializeLevelSelectMenu(lastPassedLevel);
    }
    public void InitializeLevelSelectMenu(int lastPassedLevel) {
        InitializeLevelMaps(lastPassedLevel);
        InitializeMapFragments(lastPassedLevel);
        InitializeLevels(lastPassedLevel);
        InitializeLevelConnects(lastPassedLevel);
    }

    private void InitializeLevelMaps(int lastPassedLevel) {
        foreach (GameObject levelMap in LevelMaps) {
            levelMap.SetActive(false);
        }

        if (lastPassedLevel <= 3) {
            LevelMaps[0].SetActive(true);
        } else if (lastPassedLevel <= 7) {
            LevelMaps[0].SetActive(true);
            LevelMaps[1].SetActive(true);
        } else if (lastPassedLevel >= 8) {
            foreach (GameObject levelMap in LevelMaps) {
                levelMap.SetActive(true);
            }
        }
    }

    private void InitializeMapFragments(int lastPassedLevel) {
        foreach (GameObject mapFragment in MapsFragments) {
            mapFragment.SetActive(false);
        }

        for (int i = 0; i <= lastPassedLevel; i++) {
            if (i < 12) {
                MapsFragments[i].SetActive(true);
            }
        }
    }

    private void InitializeLevels(int lastPassedLevel) {
        foreach (GameObject level in Levels) {
            level.SetActive(false);
        }

        if (lastPassedLevel == -1) {

        } else {
            for (int i = 0; i <= lastPassedLevel; i++) {
                if (i < 12) {
                    Levels[i].SetActive(true);
                }
            }
        }

        Levels[12].SetActive(true);
    }

    private void InitializeLevelConnects(int lastPassedLevel) {
        foreach (GameObject levelConnect in LevelConnects) {
            levelConnect.SetActive(false);
        }

        int lastDisplayedLevel = lastPassedLevel + 1;
        if (lastDisplayedLevel > 12) {
            lastDisplayedLevel = 12;
        }

        for (int i = 0; i < lastDisplayedLevel * 2; i++) {
            LevelConnects[i].SetActive(true);
        }

        if (lastDisplayedLevel >= 1) {
            LevelConnects[24].SetActive(true);
            LevelConnects[25].SetActive(true);
        }

    }

}
