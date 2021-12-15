using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

  static EventManager _instance = null;

  public delegate void ItemsChangedAction(List<GameItem> items);
  public static event ItemsChangedAction OnItemsChanged;

  public delegate void ToogleSafeUIAction(bool to);
  public static event ToogleSafeUIAction OnToogleSafeUI;

  public static void ItemsChanged (List<GameItem> items) {
    if (OnItemsChanged != null) OnItemsChanged(items);
  }
  public static void ToogleSafeUI (bool to) {
    if (OnToogleSafeUI != null) OnToogleSafeUI(to);
  }

  private void Awake() {
    if (EventManager._instance != this) {
      Destroy(gameObject);
      return;
    }
    EventManager._instance = this;
  }
}
