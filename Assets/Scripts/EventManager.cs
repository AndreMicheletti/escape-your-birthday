using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

  static EventManager _instance = null;

  public delegate void ItemsChangedAction(List<GameItem> items);
  public static event ItemsChangedAction OnItemsChanged;

  public delegate void ToogleSafeUIAction(bool to);
  public static event ToogleSafeUIAction OnToogleSafeUI;

  public delegate void GhostSeenAction();
  public static event GhostSeenAction OnSeenGhost;

  public static void ItemsChanged (List<GameItem> items) {
    if (OnItemsChanged != null) OnItemsChanged(items);
  }

  public static void ToogleSafeUI (bool to) {
    if (OnToogleSafeUI != null) OnToogleSafeUI(to);
  }

  public static void SeenGhost () {
    if (OnSeenGhost != null) OnSeenGhost();
  }

  private void Awake() {
    if (EventManager._instance != this) {
      Destroy(gameObject);
      return;
    }
    EventManager._instance = this;
  }
}
