using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : ClickAction
{
    public Transform targetTransform;
    public Vector3 targetPos;
    public float speed = 1.0f;

    public Player player;

    public string requiredItem = "";

    public bool isToggle = false;

    bool activated = false;

    bool moving = false;

    Vector3 initialPos;

    private void Start() {
        tag = "Clickable";
        initialPos = targetTransform.localPosition;
    }

    public override bool canUse()
    {
        if (moving) return false;
        if (requiredItem != "" && !player.hasItem(requiredItem)) return false;
        return isToggle ? true : !activated;
    }

    // Start is called before the first frame update
    public override void OnUsed()
    {
        if (!canUse()) return;
        if (moving || (activated && !isToggle)) {
            return;
        }
        if (!activated) {
            Debug.Log("DOING MOVE ACTION. DOING");
            Debug.Log(initialPos);
            moving = true;
            activated = true;
        } else {
            Debug.Log("UNDOING MOVE ACTION");
            Debug.Log(initialPos);
            moving = true;
            activated = false;
        }
    }

    private void Update() {
        if (moving) {
            Vector3 from = targetTransform.localPosition;
            Vector3 to = activated ? targetPos : initialPos;

            targetTransform.localPosition = Vector3.Lerp (from, to, Time.deltaTime * speed);
 
            if (Vector3.Distance (from, to) < 0.02 && moving)
                moving = false;
        }
    }
}
