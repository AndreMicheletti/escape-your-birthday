using System.Collections;
using UnityEngine;

public class FinalInteract : MonoBehaviour, IInteractible
{
  private bool activated = false;

  public bool CanInteract () {
    return !activated;
  }

  public string GetActionText() {
    return "interact";
  }
  
  public void OnInteract(Player player) {
    if (activated) return;
    activated = true;
    player.Pause();
    player.HideCursor();
    DialogManager._instance.dialogText.ClearDialog();
    DialogManager._instance.interactText.gameObject.SetActive(false);
    StartCoroutine(FinalDialogCoroutine());
  }

  private IEnumerator FinalDialogCoroutine() {
    yield return new WaitForSeconds(1f);
    DialogManager.ShowDialog("I'm here to torment you", 4f, 0.19f);
    yield return new WaitForSeconds(10f);
    DialogManager.ShowDialog("You were not strong", 3f, 0.22f);
    yield return new WaitForSeconds(8f);
    DialogManager.ShowDialog("Even now", 2f, 0.3f);
    yield return new WaitForSeconds(6f);
    DialogManager.ShowDialog("You can't confront me...", 3f, 0.23f);
    yield return new WaitForSeconds(9f);
    DialogManager.ShowDialog("...son", 6f, 0.8f);
    yield return new WaitForSeconds(10f);
    FadeController.FadeOut();
    yield return new WaitForSeconds(1f);
    GameStateManager.LoadGameOver();
  }
}
