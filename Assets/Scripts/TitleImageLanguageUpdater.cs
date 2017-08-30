using UnityEngine;
using UnityEngine.UI;

public class TitleImageLanguageUpdater : MonoBehaviour {

    public GameObject ZH_CN_Image;
    public GameObject EN_Image;
    private GameManager gameManager;

    void OnEnable() {
        if (gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        UpdateImage();
    }

    public void UpdateImage() {

        switch (gameManager.LanguageEnum) {
            case Language.LanguageEnum.ZH_CN:
                ZH_CN_Image.SetActive(true);
                EN_Image.SetActive(false);
                break;
            case Language.LanguageEnum.ZH_HK:
                ZH_CN_Image.SetActive(true);
                EN_Image.SetActive(false);
                break;
            default:
                ZH_CN_Image.SetActive(false);
                EN_Image.SetActive(true);
                break;
        }
    }
}
