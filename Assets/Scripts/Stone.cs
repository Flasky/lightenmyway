using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public bool DestroyingSelf = false;
    public GameObject questionMark;
    public GameObject distanceMark;
    private bool showingQuestionMark = false;
    private bool showingDistanceMark = false;

    void Start() {
        questionMark.SetActive(false);
        distanceMark.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ShowDistanceMark();
        }
    }

    public void ShowQuestionMark() {
        if (!showingQuestionMark && !showingDistanceMark) {
            showingQuestionMark = true;
            StartCoroutine(ShowMarkCoroutine(questionMark, true, false));
        }
    }

    public void ShowDistanceMark() {
        if (!showingDistanceMark && !showingQuestionMark) {
            showingDistanceMark = true;
            StartCoroutine(ShowMarkCoroutine(distanceMark, false, true));
        }
    }

    IEnumerator ShowMarkCoroutine(GameObject mark, bool shouldShowQuestionMark, bool shouldShowDistanceMark) {
        mark.SetActive(true);
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        sprites.Add(mark.GetComponent<SpriteRenderer>());
        sprites.Add(mark.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>());

        if (shouldShowQuestionMark) {
            sprites.Add(mark.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>());
        }

        float stepTime = 0.015f;
        float duration = 0.3f;
        Color transparent = new Color(1f, 1f, 1f, 0f);

        foreach (SpriteRenderer sprite in sprites) {
            sprite.color = transparent;
        }

        for (float time = 0f; time < duration; time += stepTime) {
            foreach (SpriteRenderer sprite in sprites) {
                sprite.color = Color.Lerp(transparent, Color.white, time/duration);
            }
            yield return new WaitForSeconds(stepTime);
        }

        yield return new WaitForSeconds(1f);

        for (float time = 0f; time < duration; time += stepTime) {
            foreach (SpriteRenderer sprite in sprites) {
                sprite.color = Color.Lerp(Color.white, transparent, time/duration);
            }
            yield return new WaitForSeconds(stepTime);
        }

        mark.SetActive(false);
        if (shouldShowQuestionMark) {
            showingQuestionMark = false;
        }

        if (shouldShowDistanceMark) {
            showingDistanceMark = false;
        }
    }

	public void DestroyStone() {
        DestroyingSelf = true;
        GetComponent<Animator>().SetTrigger("Destroy");
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
