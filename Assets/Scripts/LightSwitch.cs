using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractible
{
    public GameObject lights = null;
    public AudioSource audioSource = null;
    private bool active = true;
    private float currentScaleY = 1f;

    private void Awake() {
      this.currentScaleY = this.transform.localScale.y;
    }

    public bool CanInteract () {
      return true;
    }

    public void OnInteract () {
      active = !active;
      transform.localScale.Set(1, active ? 1.3f : -1.3f, 1);
      lights.SetActive(active);
      audioSource.Play();
    }

    public string GetActionText() {
      return "turn " + (active ? "off" : "on");
    }
}
