using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLanguageUpdater : MonoBehaviour {

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    void OnEnable() {
        if (gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        UpdateImage();
    }

    public void UpdateImage() {
        switch (gameManager.LanguageEnum) {
            case Language.LanguageEnum.ZH_CN:
                spriteRenderer.sprite = sprites[1];
                break;
            case Language.LanguageEnum.ZH_HK:
                spriteRenderer.sprite = sprites[2];
                break;
            default:
                spriteRenderer.sprite = sprites[0];
                break;
        }
    }
}
