using UnityEngine;

public class CollectItem : MonoBehaviour, IInteractible {

  public GameItem item = null;
  public AudioSource audioSource = null;
  private bool collected = false;

  public bool CanInteract () {
    return item != null && !collected;
  }

  public void OnInteract (Player player) {
    if (!CanInteract()) return;
    player.AddItem(item);
    if (audioSource) audioSource.Play();
    transform.position = new Vector3(0, -100, 0);
    collected = true;
  }

  public string GetActionText() {
    return "pick up " + item.itemName;
  }

  public string GetDialogText() {
    return "";
  }
}