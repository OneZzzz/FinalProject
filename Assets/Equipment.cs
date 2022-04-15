using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Equipment : Item
{
    KeyCode effectKey;
    enum activateType {Press, Hold};
    activateType thisActivateType;

    public Equipment(string equipName)
    {
        sprite = Sprite.Create((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Equipments/" + equipName + ".png", typeof(Texture2D)), new Rect(0, 0, 100, 100), new Vector2(0, 0));
    }
}
