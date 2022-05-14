using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Charm : Item
{
    public int price;
    public int space;
    static Dictionary<string, string> NameToDisplayName = new Dictionary<string, string>()
    {
        {"CharmA","Strength of Socere"}, // jump higher
        {"CharmB","Eledem's Element"}, // attack jump
        {"CharmC","Will's Whisper"}, // killing enemy restores health
        {"CharmD","Power of Plunia "}, // distant attack
        {"CharmE","Liu's Legacy"}, //blessingss
    };
    static Dictionary<string, string> NameToDescription = new Dictionary<string, string>()
    {
        {"CharmA","The holder of Socere's strength can jump in the air like a faring bird. [You can jump higher]"},
        {"CharmB","The history is written that Senior Eledem has the bizarre skill to step and jump on soley air.  [You can double jump while in air]"},
        {"CharmC","Elder Will blesses the brave with health recover after a battle. [You would recover health when you defeat an enemy]"},
        {"CharmD","God of war Plunia shoots from thousand miles away. [You can press and hold [K] to fire in distance]"},
        {"CharmE","Developer Liu wishes you good luck in your adventurous journey!"},
    };
    static Dictionary<string, int> NameToPrice = new Dictionary<string, int>()
    {
        {"CharmA",5},
        {"CharmB",10},
        {"CharmC",10},
        {"CharmD",10},
        {"CharmE",20},
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
        sprite = Resources.Load<Sprite>("Charms/" + charmName);
        displayName = NameToDisplayName[itemName];
        description = NameToDescription[itemName];
        price = NameToPrice[itemName];
        space = NameToSpace[itemName];
    }


}
