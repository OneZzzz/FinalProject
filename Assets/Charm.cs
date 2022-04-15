using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Charm : Item
{
    public int price;
    public int space;
    static Dictionary<string, string> NameToDescription = new Dictionary<string, string>()
    {
        {"CharmA","This is CharmA, it help you sober up after taking 20 shots."},
        {"CharmB","This is CharmB, it allows you to walk on water."},
        {"CharmC","This is CharmC, it is very useless, trust me, it really is."},
        {"CharmD","This is CharmD, if you do not equip it, you will not pass your exam"},
        {"CharmE","This is CharmE, it gets you out of the quarantine."},
    };
    static Dictionary<string, int> NameToPrice = new Dictionary<string, int>()
    {
        {"CharmA",50},
        {"CharmB",100},
        {"CharmC",150},
        {"CharmD",120},
        {"CharmE",200},
    };
    static Dictionary<string, int> NameToSpace = new Dictionary<string, int>()
    {
        {"CharmA",1},
        {"CharmB",2},
        {"CharmC",3},
        {"CharmD",2},
        {"CharmE",1},
    };

    public Charm(string charmName)
    {
        itemName = charmName;
        sprite = Sprite.Create((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Charms/" + charmName + ".png", typeof(Texture2D)), new Rect(0, 0, 100, 100), new Vector2(0, 0));
        description = NameToDescription[itemName];
        price = NameToPrice[itemName];
        space = NameToSpace[itemName];
    }


}
