using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    public bool DestroyingSelf = false;
    public GameObject questionMark;
    private bool showingQuestionMark = false;

    void Start() {
        questionMark.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ShowQuestionMark();
        }
    }

    public void ShowQuestionMark() {
        if (!showingQuestionMark) {
            showingQuestionMark = true;
            StartCoroutine(QuestionMarkCoroutine());
        }
    }

    IEnumerator QuestionMarkCoroutine() {
        questionMark.SetActive(true);
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        sprites.Add(questionMark.GetComponent<SpriteRenderer>());
        sprites.Add(questionMark.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>());
        sprites.Add(questionMark.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>());

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

        questionMark.SetActive(false);
        showingQuestionMark = false;
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
