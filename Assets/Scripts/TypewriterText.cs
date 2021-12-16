using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour
{

    public List<AudioClip> keyClips = new List<AudioClip>();
    public AudioClip spacebarClip = null;
    public float keyInverval = 0.2f;
    private Text label = null;
    private AudioSource audioSource = null;
    private Coroutine coroutine = null;

    private void Awake() {
      audioSource = GetComponent<AudioSource>();
      label = GetComponent<Text>();
      label.text = "";
      ShowText("this is a test to show a dialog using typewriter", 2f);
    }

    public void ShowText(string text, float afterDuration, bool forceStop = false) {
      if (coroutine != null) {
        if (forceStop) StopCoroutine(coroutine);
        else return;
      }
      coroutine = StartCoroutine(TypewriteCoroutine(text, afterDuration));
    }

    private IEnumerator TypewriteCoroutine (string text, float after) {
      for (int i = 0; i < text.Length; i++)
      {
        if (text[i].ToString() == " ") PlayClip(spacebarClip);
        else PlayClip();
        yield return new WaitForSeconds(keyInverval);
        label.text += text[i];
      }
      yield return new WaitForSeconds(after);
      coroutine = null;
    }

    private void PlayClip (AudioClip clip = null) {
      if (clip != null) audioSource.clip = clip;
      else {
        int idx = Random.Range(0, keyClips.Count);
        AudioClip selected = keyClips[idx];
        audioSource.clip = selected;
      }
      audioSource.Play();
    }
}
