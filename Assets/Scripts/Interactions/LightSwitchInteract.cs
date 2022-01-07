using UnityEngine;

public class LightSwitchInteract : MonoBehaviour, IInteractible
{
    public GameObject lights = null;
    public GameObject wallWritings = null;
    public AudioSource audioSource = null;
    private bool active = true;
    private float currentScaleY = 1f;

    private void Awake() {
      this.currentScaleY = this.transform.localScale.y;
      active = lights.activeSelf;
    }

    public bool CanInteract () {
      return true;
    }

    public void OnInteract (Player _player) {
      active = !active;
      transform.localScale.Set(1, active ? 1.3f : -1.3f, 1);
      lights.SetActive(active);
      wallWritings.SetActive(!active);
      audioSource.Play();
      if (!GameStateManager._instance.gameOverAllowed) {
        GameStateManager._instance.gameOverAllowed = true;
        _player.Resume();
      }
    }

    public string GetActionText() {
      return "turn " + (active ? "off" : "on");
    }
    public string GetDialogText() {
      return "";
    }
}
