using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
  public GameObject itemsParent = null;

  int GetItemID (GameItem item) {
    return item.id;
  }

  private void OnItemsChanged (List<GameItem> items) {
    List<int> ids = items.ConvertAll<int>(GetItemID);
    int children = itemsParent.transform.childCount;
    for (int i = 0; i < children; i++)
    {
      itemsParent.transform.GetChild(i).gameObject.SetActive(ids.Contains(i));
    }
  }

  private void OnEnable() {
    EventManager.OnItemsChanged += OnItemsChanged;
  }

  private void OnDisable() {
    EventManager.OnItemsChanged -= OnItemsChanged;
  }
}
