using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
  public string sceneName = "";
  public LoadSceneMode mode = LoadSceneMode.Single;
  
  public void Activate() {
    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
  }
}
