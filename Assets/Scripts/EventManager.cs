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
  public delegate void ReceiveFinalItemAction();
  public static event ReceiveFinalItemAction OnReceivedFinalItem;

  public static void ItemsChanged (List<GameItem> items) {
    if (OnItemsChanged != null) OnItemsChanged(items);
  }

  public static void ToogleSafeUI (bool to) {
    if (OnToogleSafeUI != null) OnToogleSafeUI(to);
  }

  public static void SeenGhost () {
    if (OnSeenGhost != null) OnSeenGhost();
  }

  public static void ReceivedFinalItem () {
    if (OnReceivedFinalItem != null) OnReceivedFinalItem();
  }

  private void Awake() {
    if (EventManager._instance != this) {
      Destroy(gameObject);
      return;
    }
    EventManager._instance = this;
  }
}
