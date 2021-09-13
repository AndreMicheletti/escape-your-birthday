using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    string codeInput = "";
    public MoveAction moveAction;
    public bool unlocked = false;
    bool coroutineRunning = false;
    public AudioSource resetAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        codeInput = "";
        tag = "Untagged";
    }

    public void onInput (string input) {
        if (unlocked) return;
        codeInput += input;
        Debug.Log(codeInput);

        if (codeInput == "0021") {
            Unlock();
        }

        if (!coroutineRunning) {
            coroutineRunning = true;
            StartCoroutine(clearInput());
        }
    }

    void Unlock () {
        unlocked = true;
        moveAction.OnUsed();
    }

    IEnumerator clearInput () {
        yield return new WaitForSeconds(15);
        codeInput = "";
        Debug.Log("CLEARING PANEL INPUT");
        coroutineRunning = false;
        if (resetAudioSource != null) {
            resetAudioSource.priority = 0;
            resetAudioSource.Play();
        }
    }
}
