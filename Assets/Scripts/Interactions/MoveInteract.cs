using UnityEngine;

public class MoveInteract : MonoBehaviour, IInteractible
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
  public string openText = "open";
  public string closeText = "close";
  public string investigateText = "investigate";
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
        Quaternion fromQuat = targetTransform.localRotation;
        Vector3 toRot = moved ? initialRot : targetRot;
        Quaternion toQuat = Quaternion.Euler(toRot);
        targetTransform.localRotation = Quaternion.RotateTowards(fromQuat, toQuat, Time.deltaTime * movementSpeed);
        rotArrived = targetTransform.localRotation == toQuat;
      }

      if (posArrived && rotArrived) {
        moving = false;
        moved = !moved;
        Debug.Log(name + " finished moving");
      }
    }
  }

  public bool CanInteract () {
    return bidirectional ? true : !moved;
  }

  public string GetActionText() {
    if (requiredItem != null && !Player._instance.HasItem(requiredItem)) return investigateText;
    if (!bidirectional && moved) return "";
    return moving ? "" : (moved ? closeText : openText);
  }

  public string GetDialogText() {
    return dialogText;
  }
}
