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
        sprite = Resources.Load<Sprite>("Charms/" + equipName);
    }

    Object[] GetSprites(string fileName)
    {
        Object[] sprites = Resources.LoadAll(fileName);
        return sprites;
    }
}
