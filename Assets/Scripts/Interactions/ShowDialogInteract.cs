using System.Collections.Generic;
using UnityEngine;

public class ShowDialogInteract : MonoBehaviour, IInteractible
{
  public List<string> dialogText = null;
  public float dialogDuration = 3f;
  public string interactText = "";
  public bool oneShot = false;
  public bool loop = false;
  private int currentIndex = 0;

  public bool CanInteract () {
    return true;
  }

  public void OnInteract(Player player) {
    if (dialogText == null) return;
    string currentDialog = dialogText[currentIndex];
    DialogManager.ShowCustomText(currentDialog, dialogDuration);
    player.ClearInteractObject();
    currentIndex += 1;
    if (currentIndex >= dialogText.Count) {
      if (loop) currentIndex = 0;
      else currentIndex = dialogText.Count-1;
      if (oneShot) transform.position = new Vector3(0, 1000, 0);
    }
  }

  public string GetActionText() {
    return interactText;
  }
}
