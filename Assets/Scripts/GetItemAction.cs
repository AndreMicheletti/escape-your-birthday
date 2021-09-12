using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemAction : ClickAction
{
    public Player player;
    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Clickable";
    }

    public override bool canUse()
    {
        return !player.hasItem(itemName);
    }

    public override void OnUsed()
    {
        Debug.Log("GET ITEM");
        player.items.Add(itemName);
        gameObject.SetActive(false);
    }
}
