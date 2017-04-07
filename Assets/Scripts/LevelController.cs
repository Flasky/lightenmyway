using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public int levelNo;
    public int lightShardCount;
    public int maxLightCount;
    public GameObject lightOnGround;

    private Player player;
    private bool placingLight = false;

    #region UI variables
    public Text lightCountText;
    public Slider sanitySlider;
    public GameObject winCanvas;
    public GameObject pauseCanvas;
    #endregion

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        lightShardCount = 0;
        UpdateLightCountText();
        Time.timeScale = 1f;

        winCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    void Update() {
        sanitySlider.value = player.GetSanityInPercentage();

        if (placingLight) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Ground"));

                if (hit.collider != null) {
                    if (lightShardCount > 0) {
                        Instantiate(lightOnGround, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion());
                        lightShardCount -= 1;
                    }
                }
            }
        }
    }

    public void TogglePlacingLight() {
        placingLight = !placingLight;
    }

	public void AddLightShard (int count) {
        lightShardCount += count;
        if (lightShardCount > maxLightCount) {
            lightShardCount = maxLightCount;
        }
        UpdateLightCountText();
    }

    public void Pause() {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void Win() {
        winCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel() {
        SceneManager.LoadScene("Level" + (levelNo + 1));
    }

    public void Replay() {
        SceneManager.LoadScene("Level" + levelNo);
    }

    void UpdateLightCountText() {
        lightCountText.text = lightShardCount.ToString();
    }
}
