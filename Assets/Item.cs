using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite sprite;
    public RectTransform uiBlock;
    public static Dictionary<string, int> allCharms = new Dictionary<string, int> { //1: aquired 0: unaquired
        {"CharmA", 0 },
        {"CharmB", 0 },
        {"CharmC", 0 },
        {"CharmD", 0 },
        {"CharmE", 0 },
    };
    public static Dictionary<string, int> allEquipments = new Dictionary<string, int> { //1: aquired 0: unaquired
    };

}
