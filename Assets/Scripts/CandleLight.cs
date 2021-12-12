using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{

  public float frequency = 1f;
  public float intensity = 1f;

  private Light lightSource;
  private float startingIntensity = 0f;

  private void Awake() {
    this.lightSource = this.GetComponent<Light>();
    this.startingIntensity = this.lightSource.intensity;
    StartCoroutine(TrembleLight());
  }

  private IEnumerator TrembleLight() {
    while (true) {
      float wait = Random.Range(0.1f, this.frequency);
      yield return new WaitForSeconds(wait);
      float variation = Random.Range(-this.intensity, this.intensity);
      this.lightSource.intensity = this.startingIntensity + variation;
    }
  }
}
