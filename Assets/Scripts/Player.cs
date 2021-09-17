using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject useLabel;
    public Text useLabelText;
    public float interactionDistance = 3f;
    public GameObject useTarget;
    public ArrayList items = new ArrayList();
    // string activeColor = "";

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Camera camera = GetComponent<Camera>();

        ClickAction targetedAction = useTarget != null ? useTarget.GetComponent<ClickAction>() : null;
        if (Input.GetMouseButtonUp(0) && targetedAction != null)
        {
            targetedAction.OnUsed();
        }

        Debug.DrawRay(transform.position, transform.forward * interactionDistance);
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, camera.cullingMask)) {
            useTarget = hit.collider.tag == "Clickable" ? hit.collider.gameObject : null;
        } else {
            useTarget = null;
        }
    }

    private void FixedUpdate() {
        ClickAction targetedAction = useTarget != null ? useTarget.GetComponent<ClickAction>() : null;
        useLabel.SetActive(targetedAction != null && targetedAction.canUse());
        if (targetedAction != null)
            useLabelText.text = "[CLICK] to " + targetedAction.getActionName();
    }

    public bool hasItem (string itemName) {
        return items.Contains(itemName);
    }
}
