using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
  public AudioSource spottedAudio = null;
  public AudioSource breathAudio = null;
  public Transform[] spawnPositions = null;
  private System.Random rnd = new System.Random();
  private SpriteRenderer spriteRenderer = null;
  private new Camera camera = null;
  private bool found = false;

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    camera = FindObjectOfType<Camera>();
    spriteRenderer.enabled = false;
  }

  public void SpawnGhost () {
    transform.position = spawnPositions[rnd.Next(spawnPositions.Length)].position;
    spriteRenderer.enabled = true;
    breathAudio.Play();
    found = false;
  }

  private void Update() {
    if (spriteRenderer.enabled && !found)
      CheckVisibility();
  }

  public void CheckVisibility() {
    Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
    bool onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
    bool raycasted = false;

    RaycastHit hit;
    if (Physics.Raycast(Player._instance.transform.position, Player._instance.transform.forward, out hit, 9999f)) {
      if (hit.collider.gameObject == gameObject) raycasted = true;
    }

    if (onScreen && spriteRenderer.isVisible && raycasted) {
      // Visible
      OnFound();
    }
  }

  private void OnFound() {
    if (found) return;
    found = true;
    breathAudio.Stop();
    StartCoroutine(HideCoroutine());
  }

  private IEnumerator HideCoroutine () {
    if (spottedAudio) spottedAudio.Play();
    yield return new WaitForSeconds(0.5f);
    EventManager.SeenGhost();
    spriteRenderer.enabled = false;
    transform.position = new Vector3(0, 0, 0);
    found = false;
  }
}
