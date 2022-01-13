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
  private System.Random rand = new System.Random(); 

  private void Awake() {
    audioSource = GetComponent<AudioSource>();
    label = GetComponent<Text>();
    label.text = "";
  }

  public void ShowText(string text, float afterDuration, bool forceStop = false) {
    if (coroutine != null) {
      if (forceStop) StopCoroutine(coroutine);
      else return;
    }
    coroutine = StartCoroutine(TypewriteCoroutine(text, afterDuration, keyInverval));
  }
  public void ShowText(string text, float afterDuration, float customKeyInterval, bool forceStop = false) {
    if (coroutine != null) {
      if (forceStop) StopCoroutine(coroutine);
      else return;
    }
    coroutine = StartCoroutine(TypewriteCoroutine(text, afterDuration, customKeyInterval));
  }

  private IEnumerator TypewriteCoroutine (string text, float after, float interval) {
    label.text = "";
    for (int i = 0; i < text.Length; i++)
    {
      if (text[i].ToString() == " ") PlayClip(spacebarClip);
      else PlayClip();
      yield return new WaitForSeconds(interval);
      label.text += text[i];
    }
    yield return new WaitForSeconds(after);
    coroutine = null;
    label.text = "";
  }

  private void PlayClip (AudioClip clip = null) {
    if (clip != null) audioSource.clip = clip;
    else {
      int idx = rand.Next(keyClips.Count);
      AudioClip selected = keyClips[idx];
      audioSource.clip = selected;
    }
    audioSource.Play();
  }

  public void ClearDialog () {
    StopCoroutine(coroutine);
    coroutine = null;
    label.text = "";
  }
}
