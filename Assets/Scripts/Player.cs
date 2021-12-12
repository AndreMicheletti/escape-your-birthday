using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float interactDistance = 1f;
    public Text helperText;
    public Image filmGrain;
    public float maxTimeInDark = 10f;

    public  AnimationCurve darkenCurve;
    public AudioSource darkSoundClip;

    public GameObject roomLights = null;
    private IInteractible interactObject = null;
    private Coroutine onDark = null;

    private void Awake() {
      if (helperText != null) helperText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
      Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

      RaycastHit hit;
      if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance)) {
        IInteractible interact = hit.collider.gameObject.GetComponent<IInteractible>();
        if (interact != null) {
          interactObject = interact;
        }
      } else if (interactObject != null) interactObject = null;
    }

    private void FixedUpdate() {
      helperText.text = (canInteract() ? "Click to " + interactObject.GetActionText() : "");

      if (onDark == null && !roomLights.activeInHierarchy) {
        StartOnDark();
      } else if (onDark != null && roomLights.activeInHierarchy) {
        StopOnDark();
      }
    }

    public void StartOnDark () {
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
    }

    private bool canInteract () {
      return (interactObject != null && interactObject.CanInteract());
    }

    public void OnInteract (InputAction.CallbackContext context) {
      if (canInteract() && context.performed) {
        interactObject.OnInteract();
      }
    }
}
