using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ClickAction : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void OnUsed();

    public abstract bool canUse();
}
