using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : ClickAction
{
    public Player player;
    public GameObject target;
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
        return !target.activeInHierarchy;
    }

    public override void OnUsed()
    {
        if (!canUse()) return;
        if (audioSource != null) {
            audioSource.priority = 0;
            audioSource.Play();
        }
        target.SetActive(true);
    }
}
