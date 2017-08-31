using UnityEngine;

/**
    Use for randomizing levels, by only displaying one single level piece from a collection
 */

public class MapManager : MonoBehaviour {

    public GameObject[] randomLevelPieces;

    private int indexToBeShown;

	void Start () {
		indexToBeShown = Random.Range(0, randomLevelPieces.Length);

        foreach (GameObject go in randomLevelPieces) {
            go.SetActive(false);
        }
        for (int i = 0; i < randomLevelPieces.Length; i++) {
            if (i == indexToBeShown) {
                randomLevelPieces[i].SetActive(true);
            }
        }
	}
}
