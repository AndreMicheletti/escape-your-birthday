using UnityEngine;

[CreateAssetMenu(fileName = "GameItem", menuName = "EscapeYourBirthdayV2/GameItem", order = 0)]
public class GameItem : ScriptableObject {
  
  public int id = 0;
  public string itemName = "";
  public Sprite guiSprite = null;

}
