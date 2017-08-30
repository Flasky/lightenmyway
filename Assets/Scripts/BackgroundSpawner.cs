using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    public int backgroundNumber;
	public GameObject ObjectToSpawn;
    public int NoOfRows;
    public int NoOfColumns;
    public float HorizontalSpacing;
    public float VerticalSpacing;
    public Vector2 OriginPosition;

    private Queue<GameObject> bgTiles = new Queue<GameObject>();
    private Queue<Sprite> sprites = new Queue<Sprite>();

    void Start() {
        string bgPath = "";

        switch (backgroundNumber) {
            case 1:
                bgPath = "1st";
                break;
            case 2:
                bgPath = "2nd";
                break;
            case 3:
                bgPath = "3rd";
                break;
            default:
                break;
        }

        string basePath = "bk_" + bgPath + "_Chapter";

        Object[] sprite = Resources.LoadAll(basePath, typeof(Sprite));
        foreach (Object o in sprite) {
            sprites.Enqueue( (Sprite) o);
        }

        Vector2 newPosition = new Vector2();
        for (int row = 0; row < NoOfRows; row++) {
            for (int column = 0; column < NoOfColumns; column++) {
                newPosition = OriginPosition + new Vector2(HorizontalSpacing * column, -VerticalSpacing * row);
                GameObject tile;
                tile = Instantiate(ObjectToSpawn, newPosition, new Quaternion(), this.transform) as GameObject;
                tile.GetComponent<SpriteRenderer>().sprite = sprites.Dequeue();
            }
        }
    }
}
