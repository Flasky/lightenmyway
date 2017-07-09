using UnityEngine;

public class EndTile : MonoBehaviour {

	public enum EndTileDirection {Up, Left, Down, Right}
    public EndTileDirection endTileDirection;

    public Sprite[] tileSprites;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (endTileDirection) {
            case EndTileDirection.Up:
                spriteRenderer.sprite = tileSprites[0];
                break;
            case EndTileDirection.Left:
                spriteRenderer.sprite = tileSprites[1];
                break;
            case EndTileDirection.Down:
                spriteRenderer.sprite = tileSprites[2];
                break;
            case EndTileDirection.Right:
                spriteRenderer.sprite = tileSprites[3];
                break;
            default:
                Debug.LogError("End tile direction setting wrong");
                break;
        }
    }
}
