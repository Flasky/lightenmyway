using UnityEngine;
using UnityEngine.UI;

public class LanguageUpdater : MonoBehaviour {

	public string KeyString;
    public Text TargetText;

    private GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateTextLanguage();
    }

    void OnEnable() {
        if (gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        UpdateTextLanguage();
    }

    public void UpdateTextLanguage() {
        if (TargetText == null) {
            TargetText = GetComponent<Text>();
        }

        TargetText.text = gameManager.language.LangDic[KeyString];
    }
}
