using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinisterMove : MonoBehaviour
{

  public float doodleIntensity = 0.09f;
  private Vector3 originalPos = new Vector3();

  private void Awake() {
    originalPos = transform.position;
  }

  void Update() {
    float offsetX = Random.Range(-doodleIntensity, doodleIntensity);
    float offsetY = Random.Range(-doodleIntensity, doodleIntensity);
    transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
  }
}
