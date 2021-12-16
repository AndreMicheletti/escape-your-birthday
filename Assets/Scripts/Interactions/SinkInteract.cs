using UnityEngine;

public class SinkInteract : MonoBehaviour, IInteractible
{

  public ParticleSystem waterFlowParticles = null;
  public AudioSource turnAudio = null;
  public AudioSource streamAudio = null;

  private bool active = false;

  private void Awake() {
    if (streamAudio) streamAudio.loop = true;
  }

  public bool CanInteract () {
    return true;
  }
  public void OnInteract(Player player) {
    if (turnAudio) turnAudio.Play();
    if (active) {
      waterFlowParticles.Stop();
      if (streamAudio) streamAudio.Stop();
    } else { 
      waterFlowParticles.Play();
      if (streamAudio) streamAudio.Play();
    }
    active = !active;
  }

  public string GetActionText() {
    return active ? "turn off the faucet" : "turn on the faucet";
  }
}
