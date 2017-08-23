using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	private Player player;
    private LevelController levelController;
    private AudioSource audioSourceForLight;
    private AudioSource audioSourceForBeating;
    public AudioClip[] beatingClips;


    // sanity
    public Slider sanitySlider;
    public GameObject sanitySliderBackground;
    public GameObject sanitySliderFill;
    public GameObject[] redCorners;
    private bool isSanityInMidRange = false;
    private bool isSanityLow = false;
    public GameObject wavePrefab;

    public GameObject crystal;
    public Text crystalCountText;
    public GameObject crystalIcon;
    public GameObject movingLightSmallPrefab;
    public AudioClip crystalPickUpSound;
    private Vector3 crystalIconPosition;
    private Vector3 lightShardPosition;

    public GameObject crystalFlower;
    public GameObject flowerCountIcon;
    public Text flowerCountText;

    public GameObject scapegoatDoll;
    public GameObject scapegoatDollIcon;

    public Sprite[] sanityIcons;
    public Sprite[] greySanityIcons;

    // Language
    public Text SkipText;
    public Text LevelText;

    public Text PauseText;

    public Text WinText;
    public Text LoseText;

    private GameManager gameManager;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSourceForLight = (AudioSource) GetComponents<AudioSource>()[0];
        audioSourceForLight.loop = false;

        audioSourceForBeating = (AudioSource) GetComponents<AudioSource>()[1];
        audioSourceForBeating.loop = true;

        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

		switch (levelController.chapterNo) {
            // chapter 0 is tutorial
            case 0:
                // hide unused icons
                crystalFlower.SetActive(false);
                scapegoatDoll.SetActive(false);
            	break;
            case 1:
                // hide unused icons
                crystalFlower.SetActive(false);
                scapegoatDoll.SetActive(false);
                break;
            case 2:
                // hide unused icons
                scapegoatDoll.SetActive(false);
                break;
            case 3:
                // do nothing
                break;
            default:
                Debug.LogError("Menu bar error in HUDManager.");
                break;
        }

        foreach (GameObject go in redCorners) {
            go.SetActive(false);
        }

        UpdateLightCountText(0);
        crystalIconPosition = crystalIcon.transform.position;

        UpdateTextLanguage();
        StartCoroutine(SanityWaveCoroutine());
    }

    void Update() {
        float sanityPercentage = player.GetSanityInPercentage();
        sanitySlider.value = sanityPercentage;

        // 70-100
		if (sanityPercentage >= 0.7f) {
			ChangeSanityIcon(sanityIcons[0], greySanityIcons[0]);
            if (audioSourceForBeating.clip != beatingClips[0]) {
                audioSourceForBeating.clip = beatingClips[0];
                audioSourceForBeating.Play();
            }

            isSanityLow = false;
            isSanityInMidRange = false;
            redCorners[0].SetActive(false);
            redCorners[1].SetActive(false);
            StopCoroutine(SanityLowCoroutine());
            StopCoroutine(SanityMidRangeCoroutine());

        // 30-70
        } else if (sanityPercentage >= 0.3f) {
            ChangeSanityIcon(sanityIcons[1], greySanityIcons[1]);
            if (player.IsSanityDropping) {
                if (audioSourceForBeating.clip != beatingClips[2]) {
                    audioSourceForBeating.clip = beatingClips[2];
                    audioSourceForBeating.Play();
                }
            } else {
                if (audioSourceForBeating.clip != beatingClips[1]) {
                    audioSourceForBeating.clip = beatingClips[1];
                    audioSourceForBeating.Play();
                }
            }

            if (!isSanityInMidRange) {
                isSanityLow = false;
                isSanityInMidRange = true;
                redCorners[1].SetActive(false);
                StopCoroutine(SanityLowCoroutine());
                StartCoroutine(SanityMidRangeCoroutine());
            }
        // 0-30
        } else if (sanityPercentage >= 0) {
            ChangeSanityIcon(sanityIcons[2], greySanityIcons[2]);
            if (audioSourceForBeating.clip != beatingClips[2]) {
                audioSourceForBeating.clip = beatingClips[2];
                audioSourceForBeating.Play();
            }
            if (!isSanityLow) {
                isSanityInMidRange = false;
                isSanityLow = true;
                redCorners[0].SetActive(false);
                StopCoroutine(SanityMidRangeCoroutine());
                StartCoroutine(SanityLowCoroutine());
            }
        }

        if (player.HasScapeGoat()) {
            scapegoatDollIcon.SetActive(true);
        } else {
            scapegoatDollIcon.SetActive(false);
        }

    }

    public void UpdateTextLanguage() {
        UpdateHUDLanguage();
        UpdatePauseLanguage();
        UpdateWinLanguage();
        UpdateLoseLanguage();
        UpdateLevelNoLanguage();
    }

    public void UpdateHUDLanguage() {
        SkipText.text = gameManager.language.LangDic["Skip Text"];

    }

    public void UpdatePauseLanguage() {
        PauseText.text = gameManager.language.LangDic["Pause"];
    }

    public void UpdateWinLanguage() {
        WinText.text = gameManager.language.LangDic["Level Clear"];
    }

    public void UpdateLoseLanguage() {
        LoseText.text = gameManager.language.LangDic["Level Failed"];
    }

    public void UpdateLevelNoLanguage() {
        switch (gameManager.LanguageEnum) {
            case Language.LanguageEnum.ZH_CN:
                if (levelController.levelNo == 0) {
                    LevelText.text = "- 教学关 -";
                } else {
                    LevelText.text = "- 第 " + levelController.levelNo + " 关 -";
                }
                break;
            case Language.LanguageEnum.ZH_HK:
                if (levelController.levelNo == 0) {
                    LevelText.text = "- 教學關 -";
                } else {
                    LevelText.text = "- 第 " + levelController.levelNo + " 關 -";
                }
                break;
            default:
                if (levelController.levelNo == 0) {
                    LevelText.text = "- Tutorial -";
                } else {
                    LevelText.text = "- Level " + levelController.levelNo + " -";
                }
                break;
        }
    }

    private void ChangeSanityIcon (Sprite newIcon, Sprite newBackground) {
		sanitySliderBackground.GetComponent<Image>().sprite = newBackground;
        sanitySliderFill.GetComponent<Image>().sprite = newIcon;
    }

    public void UpdateLightCountText(int lightCount) {
        if (lightCount == 0) {
            crystalIcon.SetActive(false);
            crystalCountText.text = "";
        } else {
            crystalIcon.SetActive(true);
            crystalCountText.text = lightCount.ToString();
        }
    }

    public void UpdateFlowerCountText(int flowerCount) {
        if (flowerCount == 0) {
            flowerCountIcon.SetActive(false);
            flowerCountText.text = "";
        } else if (flowerCount < 2) {
            flowerCountIcon.SetActive(true);
            flowerCountText.text = flowerCount.ToString();
        } else if (flowerCount == 2) {
            flowerCountIcon.SetActive(true);
            flowerCountText.text = "MAX";
        }
    }

    public void PickUpLightShard(int number, Vector3 lightWorldPosition) {
        Vector3 lightScreenPosition = Camera.main.WorldToScreenPoint(lightWorldPosition);
        StartCoroutine(LightShardManagerCoroutine(number, lightScreenPosition));
    }

    IEnumerator LightShardManagerCoroutine(int number, Vector3 lightScreenPosition) {
        for (int i = 0; i < number; i++) {
            StartCoroutine(LightShardFlyToIconCoroutine(lightScreenPosition));
            yield return new WaitForSeconds(0.21f);
        }
    }

    IEnumerator LightShardFlyToIconCoroutine(Vector3 lightScreenPosition) {
        Vector3 currentScreenPosition = lightScreenPosition;
        Vector3 currentWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenPosition);
        GameObject smallMovingLight = Instantiate(movingLightSmallPrefab, new Vector3(currentWorldPosition.x, currentWorldPosition.y, 0f), new Quaternion());
        float startTime = Time.time;
        float duration = 0.3f;
        float passedTime;

        while ((Time.time - startTime) < duration) {
            passedTime = Time.time - startTime;
            currentScreenPosition = Vector2.Lerp(lightScreenPosition, crystalIconPosition, passedTime/duration);
            currentWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenPosition);
            smallMovingLight.transform.position = new Vector3(currentWorldPosition.x, currentWorldPosition.y, 0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        audioSourceForLight.clip = crystalPickUpSound;
        audioSourceForLight.Play();
        StartCoroutine(SanityIconEnlargeCoroutine());
        Destroy(smallMovingLight);

    }

    IEnumerator SanityIconEnlargeCoroutine() {
        float startTime = Time.time;
        float duration = 0.1f;
        float passedTime;
        Vector3 origintalSize = sanitySlider.GetComponent<RectTransform>().localScale;
        Vector3 largeSize = new Vector3(1.2f, 1.2f, 1.2f);

        while ((Time.time - startTime) < duration) {
            passedTime = Time.time - startTime;
            sanitySlider.GetComponent<RectTransform>().localScale = Vector3.Lerp(origintalSize, largeSize, passedTime/duration);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        startTime = Time.time;
        while ((Time.time - startTime) < duration) {
            passedTime = Time.time - startTime;
            sanitySlider.GetComponent<RectTransform>().localScale = Vector3.Lerp(largeSize, origintalSize, passedTime/duration);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    IEnumerator SanityMidRangeCoroutine() {
        float stepTime = 0.015f;
        float duration = 0.3f;
        Color transparent = new Color(1f, 1f, 1f, 0f);

        redCorners[0].SetActive(true);
        redCorners[0].GetComponent<Image>().color = transparent;

        while (true) {
            for (float time = 0f; time < duration; time += stepTime) {
                redCorners[0].GetComponent<Image>().color = Color.Lerp(transparent, Color.white, time/duration);
                yield return new WaitForSeconds(stepTime);
            }

            for (float time = 0f; time < duration; time += stepTime) {
                redCorners[0].GetComponent<Image>().color = Color.Lerp(Color.white, transparent, time/duration);
                yield return new WaitForSeconds(stepTime);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SanityLowCoroutine() {
        float stepTime = 0.015f;
        float duration = 0.2f;
        Color transparent = new Color(1f, 1f, 1f, 0f);

        redCorners[1].SetActive(true);
        redCorners[1].GetComponent<Image>().color = transparent;

        while (true) {
            for (float time = 0f; time < duration; time += stepTime) {
                redCorners[1].GetComponent<Image>().color = Color.Lerp(transparent, Color.white, time/duration);
                yield return new WaitForSeconds(stepTime);
            }

            for (float time = 0f; time < duration; time += stepTime) {
                redCorners[1].GetComponent<Image>().color = Color.Lerp(Color.white, transparent, time/duration);
                yield return new WaitForSeconds(stepTime);
            }

            //yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SanityWaveCoroutine() {
        while (true) {
            if (player.IsSanityStatic) {
                yield return new WaitForSeconds(Time.deltaTime);
            } else {
                GameObject wave = Instantiate(wavePrefab, sanitySlider.transform.Find("Waves")) as GameObject;
                if (player.IsSanityDropping) {
                    wave.GetComponent<Wave>().Initialize(Color.red);
                }
                else if (player.IsSanityIncreasing) {
                    wave.GetComponent<Wave>().Initialize(new Color(0f, 244f/255f, 1f, 1f));
                } else {
                    Destroy(wave.gameObject);
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
