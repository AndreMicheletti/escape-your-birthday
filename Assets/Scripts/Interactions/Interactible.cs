public interface IInteractible {
  bool CanInteract ();
  void OnInteract(Player player);
  string GetActionText();
}
