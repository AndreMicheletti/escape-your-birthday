using UnityEngine;

public class SafeInteract : MonoBehaviour, IInteractible
{

  public Transform safeDoor = null;
  public GameObject roomLights = null;

  public bool CanInteract () {
    return roomLights.activeInHierarchy == true;
  }

  public void OnInteract(Player player) { 
    EventManager.ToogleSafeUI(true);
  }

  public void OpenSafe () {
    safeDoor.localEulerAngles = new Vector3(0, -150, 0);
    gameObject.SetActive(false);
  }

  public string GetActionText() {
    return "interact with Safe";
  }
}
