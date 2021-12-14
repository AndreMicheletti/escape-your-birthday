using UnityEngine;
using UnityEngine.UI;

public class SafeUI : MonoBehaviour {
  
  public Text visorText = null;
  public Button enterButton = null;
  public Button backspaceButton = null;
  public Button[] numberButtons = {};
  public string secret = "";
  public AudioClip inputSound = null;
  public AudioClip removeSound = null;
  public AudioClip errorSound = null;
  public AudioClip successSound = null;
  private AudioSource audioSource = null;

  private string currentText = "";

  private void Awake () {
    audioSource = GetComponent<AudioSource>();
    if (audioSource != null) {
      audioSource.volume = 0.7f;
      audioSource.playOnAwake = false;
      audioSource.loop = false;
    }
    Hide();
  }

  public void Show () {
    gameObject.SetActive(true);
    currentText = "";
  }

  public void Hide () {
    gameObject.SetActive(false);
  }

  // Buttons
  public void OnInputNumber (string number) {
    if (currentText.Length >= 6) return;
    currentText += number;
  }

  public void OnBackspace () {
    if (currentText.Length < 1) return;
    if (currentText.Length == 1) currentText = "";
    else currentText.Remove(currentText.Length - 2);
  }

  public void OnEnter () {
    if (currentText == secret) {

    }
  }

  // Sound
  private void PlaySound(AudioClip clip) {
    if (audioSource == null || clip == null) return;
    audioSource.clip = clip;
    audioSource.Play();
  }
}