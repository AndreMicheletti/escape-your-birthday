using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : ClickAction
{
    public Player player;
    public GameObject target;
    public string requiredItem = "";
    public AudioSource audioSource;
    public AudioSource deniedAudioSource;
    bool expired = false;
    public string actionName;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Clickable";
    }
    public override string getActionName()
    {
        return "interact";
    }

    public override bool canUse()
    {
        if (requiredItem != "" && !player.hasItem(requiredItem)) return false;
        return !expired && !target.activeInHierarchy;
    }

    public override void OnUsed()
    {
        if (!canUse()) {
            if (deniedAudioSource != null) {
                deniedAudioSource.priority = 0;
                deniedAudioSource.Play();
            }
            return;
        }
        if (audioSource != null) {
            audioSource.priority = 0;
            audioSource.Play();
        }
        target.SetActive(true);
        expired = true;
    }
}
