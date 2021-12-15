using UnityEngine;

public class SafeInteract : MonoBehaviour, IInteractible
{
  public bool CanInteract () {
    return true;
  }

  public void OnInteract(Player player) { 
    EventManager.ToogleSafeUI(true);
  }

  public string GetActionText() {
    return "interact with Safe";
  }
}
