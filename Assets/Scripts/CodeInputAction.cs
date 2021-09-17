using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInputAction : ClickAction
{
    public string buttonName;
    public CodePanel codePanel;
    public string inputValue;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Clickable";
    }

    public override string getActionName()
    {
        return "press " + buttonName;
    }

    public override void OnUsed () {
        if (!canUse()) return;
        codePanel.onInput(inputValue);
        if (audioSource != null) {
            audioSource.priority = 0;
            audioSource.Play();
        }
    }

    public override bool canUse () {
        return !codePanel.unlocked;
    }
}
