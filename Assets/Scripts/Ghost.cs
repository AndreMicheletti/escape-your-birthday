using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
  public AudioSource spottedAudio = null;
  public AudioSource breathAudio = null;
  public Transform[] spawnPositions = null;
  public float doodleIntensity = 0.05f;
  private System.Random rnd = new System.Random();
  private SpriteRenderer spriteRenderer = null;
  private new Camera camera = null;
  private bool found = false;
  private Vector3 originalPos = new Vector3();
  private Coroutine fading = null;

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    camera = FindObjectOfType<Camera>();
    spriteRenderer.enabled = false;
  }

  public void SpawnGhost () {
    StartCoroutine(TentativeSpawn());
  }
  public void SpawnGhost (int index) {
    Transform selected = spawnPositions[index];
    originalPos = selected.position;
    transform.position = selected.position;
    spriteRenderer.enabled = true;
    fading = StartCoroutine(FadeIn());
    breathAudio.Play();
    found = false;
  }

  private IEnumerator TentativeSpawn() {
    found = true;
    spriteRenderer.enabled = true;
    spriteRenderer.color = new Color(1, 1, 1, 0);
    bool onScreen = true;
    do {
      Debug.Log("Tentative spawn...");
      Transform selected = spawnPositions[rnd.Next(spawnPositions.Length)];
      originalPos = selected.position;
      transform.position = selected.position;
      Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
      onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
      yield return new WaitForSeconds(0.1f);
    } while (onScreen == true);
    fading = StartCoroutine(FadeIn());
    breathAudio.Play();
    found = false;
  }

  private IEnumerator FadeIn () {
    float factor = 100f;
    float totalTime = 0.3f;
    float step = totalTime / factor;
    spriteRenderer.color = new Color(1, 1, 1, 0);
    for (float i = 0f; i < factor; i += 1) {
      yield return new WaitForSeconds(step);
      Color newColor = new Color(1, 1, 1, (i / factor) * 0.75f);
      spriteRenderer.color = newColor;
    }
    fading = null;
  }

  private void Update() {
    if (spriteRenderer.enabled && !found) {
      if (CheckVisibility()) OnFound();
      float offsetX = Random.Range(-doodleIntensity, doodleIntensity);
      float offsetY = Random.Range(-doodleIntensity, doodleIntensity);
      transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
    }
  }

  public bool CheckVisibility() {
    Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
    bool onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
    bool raycasted = false;

    RaycastHit hit;
    if (Physics.Raycast(Player._instance.transform.position, Player._instance.transform.forward, out hit, 9999f)) {
      if (hit.collider.gameObject == gameObject) raycasted = true;
    }

    return (onScreen && spriteRenderer.isVisible && raycasted);
  }

  public void OnFound(bool playAudio = true) {
    if (found) return;
    if (fading != null) StopCoroutine(fading);
    if (spottedAudio && playAudio) spottedAudio.Play();
    fading = null;
    found = true;
    breathAudio.Stop();
    EventManager.SeenGhost();
    StartCoroutine(HideCoroutine());
  }

  private IEnumerator HideCoroutine () {
    yield return new WaitForSeconds(0.5f);
    spriteRenderer.enabled = false;
    transform.position = new Vector3(0, 0, 0);
    found = false;
  }
}
