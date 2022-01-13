using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDialog : MonoBehaviour
{
  private bool activated = false;

  private void OnTriggerEnter(Collider other) {
    if (activated) return;
    if (other.gameObject.CompareTag("Player")) {
      Player._instance.firstPerson.enableAudioSFX = false;
      StartCoroutine(ShowDialogCoroutine());
    }
  }

  private IEnumerator ShowDialogCoroutine() {
    activated = true;
    yield return new WaitForSeconds(0.5f);
    DialogManager.ShowDialog("You have to face me...", 2f, 0.18f);
    yield return new WaitForSeconds(6f);
    DialogManager.HideDialog();
  }
}
