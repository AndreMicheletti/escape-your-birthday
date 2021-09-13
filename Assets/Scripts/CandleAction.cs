using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAction : ClickAction
{
    public Player player;
    public GameObject fire;
    bool used = false;

    private void Start() {
        tag = "Clickable";
    }

    // Start is called before the first frame update
    public override void OnUsed() {
        if (!canUse()) return;
        fire.SetActive(true);
        used = true;
        StartCoroutine(consumeCandle());
    }

    IEnumerator consumeCandle()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }

    public override bool canUse() {
        return !used && player.hasItem("Lighter");
    }
}
