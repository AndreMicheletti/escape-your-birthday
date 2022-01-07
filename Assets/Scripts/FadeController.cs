using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController _instance = null;
    private Animator animator = null;

    // Start is called before the first frame update
    private void Awake() {
      if (_instance != null) {
        Destroy(gameObject);
        return;
      }
      _instance = this;
      this.animator = GetComponent<Animator>();
    }

    public static void FadeIn() {
      _instance._FadeIn();
    }
    public static void FadeOut() {
      _instance._FadeOut();
    }

    public void _FadeIn() {
      if (animator.GetCurrentAnimatorStateInfo(0).IsName("NORMAL")) return;
      animator.Play("FadeIn");
    }

    public void _FadeOut() {
      if (animator.GetCurrentAnimatorStateInfo(0).IsName("BLACK")) return;
      animator.Play("FadeOut");
    }
}
