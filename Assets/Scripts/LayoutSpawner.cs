using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSpawner : MonoBehaviour {

	public GameObject ObjectToSpawn;
    public int NoOfRows;
    public int NoOfColumns;
    public float HorizontalSpacing;
    public float VerticalSpacing;
    public Vector2 OriginPosition;

    void Start() {
        Vector2 newPosition = new Vector2();
        for (int row = 0; row < NoOfRows; row++) {
            for (int column = 0; column < NoOfColumns; column++) {
                newPosition = OriginPosition + new Vector2(HorizontalSpacing * column, -VerticalSpacing * row);
                Instantiate(ObjectToSpawn, newPosition, new Quaternion(), this.transform);
            }
        }
    }
}
