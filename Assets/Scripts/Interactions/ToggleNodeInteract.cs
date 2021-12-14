using UnityEngine;

public class ToggleNodeInteract : MonoBehaviour, IInteractible
{

  public GameItem requiredItem = null;
  public GameObject[] targets = {};
  public string canInteractText = "";
  public string cannotInteractText = "";
  public string dialogText = "";
  public AudioSource audioSource = null;

  public bool CanInteract () {
    if (requiredItem == null) return true;
    return Player._instance.HasItem(requiredItem);
  }

  public void OnInteract(Player player) {
    if (!CanInteract()) {
      DialogManager.ShowCustomText(GetDialogText(), 3f);
    } else {
      foreach (GameObject target in targets) {
        target.SetActive(!target.activeInHierarchy);
      }
      transform.position = new Vector3(0, 1000, 0);
      if (audioSource) audioSource.Play();
    }
  }

  public string GetActionText() {
    return CanInteract() ? canInteractText : cannotInteractText;
  }

  public string GetDialogText() {
    return dialogText;
  }
}
