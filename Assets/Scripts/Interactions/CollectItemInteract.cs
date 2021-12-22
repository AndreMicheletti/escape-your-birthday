using UnityEngine;

public class CollectItemInteract : MonoBehaviour, IInteractible {

  public GameItem item = null;
  public GameItem requiredItem = null;
  public AudioSource audioSource = null;
  public AudioSource[] additionalAudio = {};
  public string missingRequiredText = null;
  public string alreadyCollectedText = "";
  public string customInteractText = "";
  public bool removeOnCollect = true;
  private bool collected = false;

  public bool CanInteract () {
    return item != null;
  }

  public void OnInteract (Player player) {
    if (!removeOnCollect && collected) {
      DialogManager.ShowCustomText(alreadyCollectedText, 2f);
      return;
    }
    if (requiredItem != null && !player.HasItem(requiredItem)) {
      DialogManager.ShowCustomText(missingRequiredText, 2f);
      return;
    }
    if (!CanInteract()) return;
    player.AddItem(item);
    if (audioSource) audioSource.Play();
    if (additionalAudio != null && additionalAudio.Length > 0)
      foreach (var audio in additionalAudio) audio.Play();
    if (removeOnCollect) transform.position = new Vector3(0, -100, 0);
    collected = true;
  }

  public string GetActionText() {
    if (collected) return "";
    if (customInteractText != "") return customInteractText;
    return "pick up " + item.itemName;
  }
}