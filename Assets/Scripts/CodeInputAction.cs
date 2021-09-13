using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInputAction : ClickAction
{

    public CodePanel codePanel;

    public string inputValue;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Clickable";
    }

    public override void OnUsed () {
        if (!canUse()) return;
        codePanel.onInput(inputValue);
    }

    public override bool canUse () {
        return !codePanel.unlocked;
    }
}
