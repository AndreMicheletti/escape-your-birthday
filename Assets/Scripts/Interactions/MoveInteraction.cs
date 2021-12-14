using UnityEngine;

public class MoveInteraction : MonoBehaviour, IInteractible
{

  public GameItem requiredItem = null;
  public Transform targetTransform = null;
  public bool movePosition = true;
  public Vector3 targetPos = new Vector3();
  public bool moveRotation = false;
  public Vector3 targetRot = new Vector3();
  public bool bidirectional = false;
  public float movementSpeed = 1.5f;
  public string dialogText = "";
  public AudioSource audioSource = null;
  private Vector3 initialPos = new Vector3();
  private Vector3 initialRot = new Vector3();
  private bool moved = false;
  private bool moving = false;

  private void Awake() {
    initialPos = targetTransform.localPosition;
    initialRot = targetTransform.localEulerAngles;
  }

  public void OnInteract(Player player) {
    if (moving) return;
    if (moved && !bidirectional) return;
    if (requiredItem != null && !player.HasItem(requiredItem)) {
      DialogManager.ShowCustomText(GetDialogText(), 3f);
      return;
    }
    moving = true;
    if (audioSource) audioSource.Play();
  }

  private void Update() {
    if (moving) {
      bool posArrived = true;
      bool rotArrived = true;

      // Move Position
      if (movePosition) {
        Vector3 from = targetTransform.localPosition;
        Vector3 to = moved ? initialPos : targetPos;
        targetTransform.localPosition = Vector3.Lerp(from, to, Time.deltaTime * movementSpeed);
        posArrived = Vector3.Distance(from, to) < 0.02f;
      }

      // Move Rotation
      if (moveRotation) {
        Vector3 fromRot = targetTransform.localEulerAngles;
        Vector3 toRot = moved ? initialRot : targetRot;
        targetTransform.localEulerAngles = Vector3.Lerp(fromRot, toRot, Time.deltaTime * movementSpeed);
        rotArrived = Vector3.Distance(fromRot, toRot) < 0.02f;
      }

      if (posArrived && rotArrived) {
        moving = false;
        moved = !moved;
      }
    }
  }

  public bool CanInteract () {
    return bidirectional ? true : !moved;
  }

  public string GetActionText() {
    if (requiredItem != null && !Player._instance.HasItem(requiredItem)) return "investigate";
    return moving ? "" : (moved ? "close" : "open");
  }

  public string GetDialogText() {
    return dialogText;
  }
}
