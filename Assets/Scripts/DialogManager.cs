using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
  public static DialogManager _instance = null;
  public GameObject dialogParent = null;
  public TypewriterText dialogText = null;
  public Text interactText = null;
  public string customText = "";
  private Coroutine hideCustomCoroutine = null;

  private void Awake() {
    if (_instance != null) {
      Destroy(gameObject);
      return;
    }
    _instance = this;
  }

  public static void ShowDialog (string text, float duration) {
    DialogManager._instance._ShowDialog(text, duration);
  }
  public static void ShowDialog (string text, float duration, float interval) {
    DialogManager._instance._ShowDialog(text, duration, interval);
  }
  public static void HideDialog () {
    DialogManager._instance._HideDialog();
  }

  public static void ShowCustomText(string text, float duration) {
    DialogManager._instance._ShowCustomText(text, duration);
  }

  private void _ShowCustomText(string text, float duration) {
    customText = text;
    if (hideCustomCoroutine != null) StopCoroutine(hideCustomCoroutine);
    hideCustomCoroutine = StartCoroutine(HideCustom(duration));
  }
  
  private void _ShowDialog(string text, float duration) {
    dialogParent.SetActive(true);
    dialogText.ShowText(text, duration);
  }
  private void _ShowDialog(string text, float duration, float interval) {
    dialogParent.SetActive(true);
    dialogText.ShowText(text, duration, interval, true);
  }
  private void _HideDialog() {
    dialogParent.SetActive(false);
  }

  private IEnumerator HideCustom (float duration) {
    yield return new WaitForSeconds(duration);
    customText = "";
    hideCustomCoroutine = null;
  }

  private void Update() {
    IInteractible interact = Player._instance.GetInteractObject();
    if (interact != null && interact.GetActionText() != "") {
      interactText.text = "Click to " + interact.GetActionText();
    } else {
      interactText.text = "";
    }
    if (customText != "") interactText.text = customText;
  }
}
