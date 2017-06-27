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

    public GameObject crystalFlower;

    public GameObject scapegoatDoll;

    public Sprite[] sanityIcons;

	void Awake() {

    }

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
                scapegoatDoll.SetActive(false);
                break;
            case 3:
                // do nothing
                break;
            default:
                Debug.LogError("Menu bar error in HUDManager.");
                break;
        }

        UpdateLightCountText(0);

    }

    void Update() {
        float sanityPercentage = player.GetSanityInPercentage();
        sanitySlider.value = sanityPercentage;

		if (sanityPercentage >= 0.7f) {
			ChangeSanityIcon(sanityIcons[0]);
        } else if (sanityPercentage >= 0.3f) {
            ChangeSanityIcon(sanityIcons[1]);
        } else if (sanityPercentage >= 0) {
            ChangeSanityIcon(sanityIcons[2]);
        }
    }

    private void ChangeSanityIcon (Sprite newIcon) {
		sanitySliderBackground.GetComponent<Image>().sprite = newIcon;
        sanitySliderFill.GetComponent<Image>().sprite = newIcon;
    }


    public void UpdateLightCountText(int lightCount) {
        crystalCountText.text = lightCount.ToString();
    }
}
