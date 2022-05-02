using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public string inputItem;
    static Inventory instance;
    public static Inventory i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }
            return instance;
        }
    }

    public List<Charm> MyCharms = new List<Charm>() { };
    public List<Equipment> MyEquipments = new List<Equipment>() { };

    public void Aquire(string theName)
    {
        if (Item.allCharms.ContainsKey(theName))
        {
            if (Item.allCharms[theName] == 0) 
            {
                MyCharms.Add(new Charm(theName));
                //UI_Inventory.i.SetUpCharms(); 
            } 
            else if (Item.allCharms[theName] == 1) print("ERROR, ITEM ALREADY AQUIRED");
            else print("ERROR ON allCharms DICTIONARY");
        }
        else if (Item.allEquipments.ContainsKey(theName)) 
        {
            if (Item.allEquipments[theName] == 0)
            {
                MyEquipments.Add(new Equipment(theName));
                //UI_Inventory.i.SetUpCharms();
            }
            else if (Item.allEquipments[theName] == 1) print("ERROR, ITEM ALREADY AQUIRED");
            else print("ERROR ON allEquipment DICTIONARY");
        } 
        else print("ERROR, ITEM DOES NOT EXIST.");

    }

    public void Lose(string theName)
    {
        if (Item.allCharms.ContainsKey(theName))
        {
            if (Item.allCharms[theName] == 0) 
            {
                MyCharms.Remove(new Charm(theName));
                UI_Inventory.i.SetUpCharms();
            } 
            else if (Item.allCharms[theName] == 1) print("ERROR, ITEM ALREADY AQUIRED");
            else print("ERROR ON allCharms DICTIONARY");
        }
        else if (Item.allEquipments.ContainsKey(theName))
        {
            if (Item.allEquipments[theName] == 0)
            {
                MyEquipments.Remove(new Equipment(theName));
                UI_Inventory.i.SetUpCharms();
            }
            else if (Item.allEquipments[theName] == 1) print("ERROR, ITEM ALREADY AQUIRED");
            else print("ERROR ON allEquipment DICTIONARY");
        }
        else print("ERROR, ITEM DOES NOT EXIST.");
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Aquire("CharmA");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Aquire("CharmB");
        if (Input.GetKeyDown(KeyCode.Alpha3)) Aquire("CharmC");
        if (Input.GetKeyDown(KeyCode.Alpha4)) Aquire("CharmD");
        if (Input.GetKeyDown(KeyCode.Alpha5)) Aquire("CharmE");
    }
}
