using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player _instance = null;
    public FirstPersonAIO firstPerson = null;
    public float interactDistance = 1f;
    public Image filmGrain;
    public float maxTimeInDark = 10f;

    public  AnimationCurve darkenCurve;
    public AudioSource darkSoundClip;

    public GameObject roomLights = null;
    private IInteractible interactObject = null;
    private Coroutine onDark = null;
    private List<GameItem> items = new List<GameItem>();

    private void Awake() {
      if (Player._instance != null) {
        Destroy(gameObject);
        return;
      }
      Player._instance = this;
      firstPerson.lockAndHideCursor = true;
      firstPerson.ControllerPause();
    }

    // Update is called once per frame
    void Update() {
      Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

      if (GameStateManager.IsPaused()) {
        interactObject = null;
        return;
      }

      RaycastHit hit;
      if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance)) {
        IInteractible interact = hit.collider.gameObject.GetComponent<IInteractible>();
        if (interact != null) interactObject = interact;
        else interactObject = null;
      } else if (interactObject != null) interactObject = null;
    }

    private void FixedUpdate() {
      if (GameStateManager.IsPaused()) return;
      if (!GameStateManager._instance.gameOverAllowed) return;
      if (onDark == null && !roomLights.activeInHierarchy) {
        StartOnDark();
      } else if (onDark != null && roomLights.activeInHierarchy) {
        StopOnDark();
      }
    }

    public void StartOnDark () {
      if (GameStateManager.IsPaused()) return;
      if (!GameStateManager._instance.gameOverAllowed) return;
      EventManager.ToogleSafeUI(false);
      onDark = StartCoroutine(OnDarkCoroutine());
      darkSoundClip.volume = 0f;
      darkSoundClip.Play();
    }

    public void StopOnDark () {
      filmGrain.gameObject.SetActive(false);
      filmGrain.color = new Color(1f, 1f, 1f, 0f);
      darkSoundClip.Stop();
      StopCoroutine(onDark);
      onDark = null;
    }

    private IEnumerator OnDarkCoroutine () {
      filmGrain.gameObject.SetActive(true);
      float breakdown = 0.1f;
      float maxTime = maxTimeInDark / breakdown;
      for (int i = 0; i <= maxTime; i++)
      {
        yield return new WaitForSecondsRealtime(breakdown);
        float pointInCurve = i / maxTime;
        float ease = darkenCurve.Evaluate(pointInCurve);
        filmGrain.color = new Color(1, 1, 1, 0.27f * ease);
        darkSoundClip.volume = ease;
      }
      // Reached game over
      filmGrain.color = new Color(1, 1, 1, 1);
      GameStateManager.GameOver();
    }

    private bool canInteract () {
      return (interactObject != null && interactObject.CanInteract());
    }

    public void OnInteract (InputAction.CallbackContext context) {
      if (GameStateManager.IsPaused()) return;
      if (interactObject != null && context.performed) {
        interactObject.OnInteract(this);
      }
    }

    /** ITEMS */
    public bool HasItem(GameItem item) {
      return items.Contains(item);
    }

    public void AddItem(GameItem item) {
      if (HasItem(item)) return;
      items.Add(item);
      EventManager.ItemsChanged(items);
    }

    public void RemoveItem(GameItem item) {
      if (!HasItem(item)) return;
      items.Remove(item);
      EventManager.ItemsChanged(items);
    }

    public IInteractible GetInteractObject () {
      return interactObject;
    }

    public void ClearInteractObject () {
      interactObject = null;
    }

    public void Pause() {
      if (!firstPerson.controllerPauseState) firstPerson.ControllerPause();
    }

    public void Resume() {
      if (firstPerson.controllerPauseState) firstPerson.ControllerPause();
    }

    /** Cursor */
    public void ShowCursor () {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    public void HideCursor () {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
}
