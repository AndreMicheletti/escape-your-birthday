using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemAction : ClickAction
{
    public Player player;
    public string itemName;
    public string requiredItem = "";
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Clickable";
    }

    public override bool canUse()
    {
        if (requiredItem != "" && !player.hasItem(requiredItem)) return false;
        return !player.hasItem(itemName);
    }

    public override void OnUsed()
    {
        if (!canUse()) return;
        if (audioSource != null) {
            audioSource.priority = 0;
            audioSource.Play();
        }
        Debug.Log("GET ITEM");
        player.items.Add(itemName);
        StartCoroutine(selfDestroy());
    }

    IEnumerator selfDestroy() {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }
}
