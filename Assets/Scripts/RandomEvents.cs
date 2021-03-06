using System.Collections;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
  public LightSwitchInteract lightSwitch = null;
  public MoveInteract eyePeekHole = null;
  public GameObject eyesNode = null;
  public MoveInteract wardrobeMove = null;
  public MoveInteract chairMove = null;
  public MoveInteract doorMove = null;
  public SinkInteract sink = null;

  public AudioSource oldManAudio = null;
  public AudioSource breathingAudio = null;
  public AudioSource doorKnockAudio = null;
  public Ghost ghost = null;
  public GameItem midgameItem = null;
  private Coroutine events = null;
  private Coroutine closePeek = null;
  private int lastEvent = -1;
  private int ghostCount = 0;
  private System.Random rand = new System.Random();
  private int[] ghostEvents = {9, 10, 12, 13, 14};

  private void OnEnable() {
    EventManager.OnSeenGhost += OnSeenGhost;
    EventManager.OnReceivedFinalItem += OnReceivedFinalItem;
  }

  private void OnDisable() {
    EventManager.OnSeenGhost -= OnSeenGhost;
    EventManager.OnReceivedFinalItem -= OnReceivedFinalItem;
  }

  private void OnReceivedFinalItem () {
    ghost.OnFound(false);
    eyesNode.SetActive(false);
    StopEvents();
    sink.OnInteract(null);
    ghost.SpawnGhost(3);
    this.StopEvents();
    gameObject.SetActive(false);
  }

  public void StartEvents() {
    if (this.events != null) return;
    this.events = StartCoroutine(ActivateEventsCoroutine());
  }

  public void StopEvents() {
    StopCoroutine(this.events);
    this.events = null;
  }

  private void OnSeenGhost () {
    eyesNode.SetActive(true);
    this.StartEvents();
  }

  private bool IsMidGame () {
    if (!midgameItem) return false;
    return Player._instance.HasItem(midgameItem);
  }

  private IEnumerator ActivateEventsCoroutine() {
    yield return new WaitForSeconds(Random.Range(2f, 4f));
    while (true) {
      if (IsMidGame()) yield return new WaitForSeconds(Random.Range(6f, 12f));
      else yield return new WaitForSeconds(Random.Range(7f, 22f));
      int selected = lastEvent;
      do {
        selected = rand.Next(14);
        if (ghostCount >= 2) selected = ghostEvents[rand.Next(ghostEvents.Length)];
      } while (lastEvent == selected);
      ghostCount += 1;
      lastEvent = selected;
      switch (selected) {
        case 0:
          Debug.Log("EVENT [Chair Move]");
          chairMove.OnInteract(null);
          break;
        case 1:
          Debug.Log("EVENT [Cough]");
          oldManAudio.Play();
          yield return new WaitForSeconds(Random.Range(1f, 3f));
          break;
        case 2:
          Debug.Log("EVENT [Light Switch]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          yield return new WaitForSeconds(Random.Range(6f, 9f));
          break;
        case 3:
          Debug.Log("EVENT [Light Switch with Eye]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          eyePeekHole.OnInteract(null);
          breathingAudio.Play();
          yield return new WaitForSeconds(Random.Range(7f, 9f));
          eyePeekHole.OnInteract(null);
          break;
        case 4:
          Debug.Log("EVENT [Sink]");
          sink.OnInteract(null);
          yield return new WaitForSeconds(Random.Range(4f, 9f));
          break;
        case 5:
          Debug.Log("EVENT [Door knock]");
          doorKnockAudio.Play();
          yield return new WaitForSeconds(Random.Range(4f, 6f));
          break;
        case 6:
          Debug.Log("EVENT [Bathroom Door]");
          doorMove.OnInteract(null);
          yield return new WaitForSeconds(12f);
          doorMove.OnInteract(null);
          yield return new WaitForSeconds(Random.Range(5f, 6f));
          break;
        case 7:
          Debug.Log("EVENT [Bathroom Door with Light]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          doorMove.OnInteract(null);
          yield return new WaitForSeconds(8f);
          doorMove.OnInteract(null);
          yield return new WaitForSeconds(Random.Range(7f, 9f));
          break;
        case 8:
          Debug.Log("EVENT [Wardrobe]");
          if (Player._instance.HasItem(doorMove.requiredItem)) {
            doorMove.OnInteract(Player._instance);
            yield return new WaitForSeconds(Random.Range(5f, 8f));
          }
          break;
        case 9:
          Debug.Log("EVENT [Ghost]");
          eyesNode.SetActive(false);
          ghost.SpawnGhost();
          this.StopEvents();
          ghostCount = 0;
          break;
        case 10:
          Debug.Log("EVENT [Ghost with Light]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          eyesNode.SetActive(false);
          ghost.SpawnGhost();
          this.StopEvents();
          ghostCount = 0;
          break;
        case 11:
          Debug.Log("EVENT [Light Switch with Eye]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          eyePeekHole.OnInteract(null);
          breathingAudio.Play();
          yield return new WaitForSeconds(Random.Range(7f, 9f));
          eyePeekHole.OnInteract(null);
          break;
        case 12:
          Debug.Log("EVENT [Ghost with Sink]");
          sink.OnInteract(null);
          eyesNode.SetActive(false);
          ghost.SpawnGhost(3);
          this.StopEvents();
          ghostCount = 0;
          break;
        case 13:
          Debug.Log("EVENT [Ghost]");
          eyesNode.SetActive(false);
          ghost.SpawnGhost();
          this.StopEvents();
          ghostCount = 0;
          break;
        case 14:
          Debug.Log("EVENT [Ghost with Light]");
          if (lightSwitch.IsActive()) lightSwitch.OnInteract(null);
          eyesNode.SetActive(false);
          ghost.SpawnGhost();
          this.StopEvents();
          ghostCount = 0;
          break;
      }
    }
  }

  private void Update() {
    if (eyePeekHole.IsMoved() && closePeek == null) closePeek = StartCoroutine(ClosePeekCoroutine());
  }

  private IEnumerator ClosePeekCoroutine() {
    yield return new WaitForSeconds(6f);
    if (eyePeekHole.IsMoved()) eyePeekHole.OnInteract(Player._instance);
    closePeek = null;
  }
}
