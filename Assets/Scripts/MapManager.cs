using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject[] randomLevelPieces;

    private int indexToBeShown;

	void Start () {
		indexToBeShown = Random.Range(0, randomLevelPieces.Length);
        for (int i = 0; i < randomLevelPieces.Length; i++) {
            if (i == indexToBeShown) {
                randomLevelPieces[i].SetActive(true);
            } else {
                randomLevelPieces[i].SetActive(false);
            }
        }
	}
}
