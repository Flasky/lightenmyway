using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	private Player player;
    private LevelController levelController;

    public Slider sanitySlider;
    public GameObject sanitySliderBackground;
    public GameObject sanitySliderFill;

    public GameObject crystal;
    public Text crystalCountText;
    public GameObject crystalIcon;
    public GameObject movingLightSmallPrefab;
    private Vector3 crystalIconPosition;
    private Vector3 lightShardPosition;

    public GameObject crystalFlower;
    public GameObject flowerCountIcon;
    public Text flowerCountText;

    public GameObject scapegoatDoll;
    public GameObject scapegoatDollIcon;

    public Sprite[] sanityIcons;
    public Sprite[] greySanityIcons;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();

        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

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
                // scapegoatDoll.SetActive(false);
                break;
            case 3:
                // do nothing
                break;
            default:
                Debug.LogError("Menu bar error in HUDManager.");
                break;
        }

        UpdateLightCountText(0);
        crystalIconPosition = crystalIcon.transform.position;
    }

    void Update() {
        float sanityPercentage = player.GetSanityInPercentage();
        sanitySlider.value = sanityPercentage;

		if (sanityPercentage >= 0.7f) {
			ChangeSanityIcon(sanityIcons[0], greySanityIcons[0]);

        } else if (sanityPercentage >= 0.3f) {
            ChangeSanityIcon(sanityIcons[1], greySanityIcons[1]);
        } else if (sanityPercentage >= 0) {
            ChangeSanityIcon(sanityIcons[2], greySanityIcons[2]);
        }

        if (player.HasScapeGoat()) {
            scapegoatDollIcon.SetActive(true);
        } else {
            scapegoatDollIcon.SetActive(false);
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
}
