using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

  public SafeUI safeUI = null;
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

  private void OnToogleSafeUI(bool to) {
    if (to == true) {
      DialogManager._instance.interactText.gameObject.SetActive(false);
      safeUI.Show();
    } else {
      DialogManager._instance.interactText.gameObject.SetActive(true);
      safeUI.Hide();
    }
  }

  private void OnEnable() {
    EventManager.OnItemsChanged += OnItemsChanged;
    EventManager.OnToogleSafeUI += OnToogleSafeUI;
  }

  private void OnDisable() {
    EventManager.OnItemsChanged -= OnItemsChanged;
    EventManager.OnToogleSafeUI -= OnToogleSafeUI;
  }
}
