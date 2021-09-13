using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject useLabel;
    public float interactionDistance = 3f;
    public GameObject useTarget;
    public ArrayList items = new ArrayList();

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * interactionDistance);

        useTarget = null;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.tag == "Clickable")
            {
                useTarget = hit.collider.gameObject;
            }
        }

        ClickAction targetedAction = useTarget != null ? useTarget.GetComponent<ClickAction>() : null;

        useLabel.SetActive(targetedAction != null && targetedAction.canUse());

        if (Input.GetMouseButtonUp(0) && targetedAction != null)
        {
            targetedAction.OnUsed();
        }
    }

    public bool hasItem (string itemName) {
        return items.Contains(itemName);
    }
}
