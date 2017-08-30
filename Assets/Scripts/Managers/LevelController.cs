using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public int chapterNo;
    public int levelNo;
    public bool isFinale = false;
    public int lightShardCount;
    public int maxLightCount;
    public GameObject lightOnGround;
    public WinLight winLight;
    public bool HasWon = false;

    private Player player;
    private bool placingLight = false;
    private GameManager gameManager;
    private CameraManager cameraManager;
    private AudioSource audioSource;

    #region UI variables
    public GameObject winCanvas;
    public GameObject pauseCanvas;
    public GameObject loseCanvas;
    public Text PauseLevelText;
    #endregion

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        lightShardCount = 0;
        Time.timeScale = 1f;

        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start() {
        gameManager.gameObject.GetComponent<Options>().UpdateSFXVolume();
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        if (placingLight) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Ground", "Tile"));

                if (hit.collider != null) {
                    if (lightShardCount > 0) {
                        Instantiate(lightOnGround, new Vector3(hit.point.x, hit.point.y, 0f), new Quaternion());
                        lightShardCount -= 1;
                        // UpdateLightCountText();
                    }
                }
            }
        }
    }

    public void TogglePlacingLight() {
        placingLight = !placingLight;
    }

    public void Pause() {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
        string str = "";
        if (levelNo < 10) {
            str = "0" + levelNo.ToString();
        }
        PauseLevelText.text = gameManager.language.LangDic["Level"] + str;
    }

    public void Resume() {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void Win() {
        if (!HasWon) {
            HasWon = true;
            StartCoroutine(WinCoroutine());
        }

        if (gameManager.LastPassedLevel < this.levelNo) {
            gameManager.UpdateSaveDate(this.levelNo);
        }
    }

    IEnumerator WinCoroutine() {
        cameraManager.ZoomToPlayer(1f, 3f);
        player.Win();
        yield return new WaitForSeconds(1f); //TODO: this should be the duration of camera animation
        audioSource.Play();
        winLight.gameObject.SetActive(true);
        winLight.LightUpWholeMaze();
        yield return new WaitForSeconds(2f);
        winCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Lose() {
        loseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SelectLevel() {
        gameManager.LoadLevel(15);
    }

    public void NextLevel() {
        if (!isFinale) {
            SceneManager.LoadScene("Level" + (levelNo + 1));
        } else {
            SceneManager.LoadScene("Level" + levelNo);
        }
    }

    public void Replay() {
        SceneManager.LoadScene("Level" + levelNo);
    }
}
