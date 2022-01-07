using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
  public static GameStateManager _instance = null;

  public float introWaitTime = 2f;
  public float gameOverWaitTime = 2f;

  [HideInInspector]
  public bool paused = false;
  public bool gameOverAllowed = false;
  private int gameOverCount = 0;

  private void Awake() {      
    if (_instance != null) {
      Destroy(gameObject);
      return;
    }
    _instance = this;
    DontDestroyOnLoad(this.gameObject);
    StartCoroutine(SceneIntro());
  }

  private IEnumerator SceneIntro() {
    Debug.Log("> game over count: " + gameOverCount);
    yield return new WaitForSeconds(introWaitTime);
    if (gameOverCount > 0) {
      // Don't show dialogs
    } else {
    }
    FadeController.FadeIn();
  }

  public static bool IsPaused() {
    return _instance.paused;
  }

  public static void ResetScene() {
    SceneManager.LoadScene("Game", LoadSceneMode.Single);
    _instance.gameOverAllowed = false;
    _instance.paused = false;
    _instance.StartCoroutine(_instance.SceneIntro());
  }

  public static void GameOver() {
    Player._instance.Pause();
    Player._instance.HideCursor();
    _instance.gameOverCount += 1;
    _instance.StartCoroutine(GameOverTimer());
  }

  public static IEnumerator GameOverTimer() {
    yield return new WaitForSeconds(_instance.gameOverWaitTime);
    ResetScene();
  }

  public static void Pause () {
    Player._instance.Pause();
    _instance.paused = true;
  }

  public static void Resume () {
    Player._instance.Resume();
    _instance.paused = false;
  }
}
