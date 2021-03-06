using UnityEngine;
using UnityEngine.UI;

public class SafeUI : MonoBehaviour {
  
  public SafeInteract safeInteract = null;
  public Text visorText = null;
  public Button enterButton = null;
  public Button backspaceButton = null;
  public Button[] numberButtons = {};
  public string secret = "";
  public AudioClip inputSound = null;
  public AudioClip removeSound = null;
  public AudioClip errorSound = null;
  public AudioClip successSound = null;
  public AudioSource audioSource = null;

  private string currentText = "";

  private void Awake () {
    if (audioSource != null) {
      audioSource.volume = 0.7f;
      audioSource.playOnAwake = false;
      audioSource.loop = false;
    }
    enterButton.onClick.AddListener(OnEnter);
    backspaceButton.onClick.AddListener(OnBackspace);
  }

  private void FixedUpdate() {
    string uiText = currentText;
    while (uiText.Length < 6) {
      uiText += "*";
    }
    visorText.text = uiText;
  }

  public void Show () {
    gameObject.SetActive(true);
    Player._instance.Pause();
    Player._instance.ShowCursor();
  }

  public void Hide () {
    gameObject.SetActive(false);
    Player._instance.Resume();
    Player._instance.HideCursor();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  // Buttons
  public void OnInputNumber (string number) {
    if (currentText.Length >= 6) return;
    PlaySound(inputSound);
    currentText += number.ToString();
  }

  public void OnBackspace () {
    if (currentText.Length < 1) return;
    if (currentText.Length == 1) currentText = "";
    else currentText = currentText.Remove(currentText.Length - 1);
    PlaySound(removeSound);
  }

  public void OnEnter () {
    if (currentText == secret) {
      PlaySound(successSound);
      safeInteract.OpenSafe();
    } else {
      PlaySound(errorSound);
    }
    EventManager.ToogleSafeUI(false);
  }

  // Sound
  private void PlaySound(AudioClip clip) {
    if (audioSource == null || clip == null) return;
    audioSource.clip = clip;
    audioSource.Play();
  }
}