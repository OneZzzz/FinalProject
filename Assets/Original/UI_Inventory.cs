using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    public bool showing = false;
    public Transform itemSlotContainer, itemSlotTemplate, equipmentDescriptionContainer, charmDescriptionContainer;
    public Transform equipmentDCDescription, equipmentDCName, charmDCDescription, charmDCName;
    static UI_Inventory instance;
    Dictionary<Vector2, Item> itemCoord = new Dictionary<Vector2, Item>();
    Dictionary<Vector2, Equipment> equipmentCoord = new Dictionary<Vector2, Equipment>();
    Vector2 selectedCoord;

    public static UI_Inventory i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UI_Inventory>();
            return instance;
        }
    }

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        equipmentDescriptionContainer = transform.Find("EquipmentDescriptionContainer");
        charmDescriptionContainer = transform.Find("CharmDescriptionContainer");
        equipmentDCName = equipmentDescriptionContainer.Find("Name");
        equipmentDCDescription = equipmentDescriptionContainer.Find("Description");
    }

    private void Start()
    {
        GetComponent<UI_Displayer>().afterHide = AfterClose;
    }

    private void Update()
    {
        if (showing && Input.GetKeyDown(KeyCode.A)) ItemMoveSelectedCoord(new Vector2(-1,0));
        if (showing && Input.GetKeyDown(KeyCode.D)) ItemMoveSelectedCoord(new Vector2(1, 0));
        if (showing && Input.GetKeyDown(KeyCode.W)) ItemMoveSelectedCoord(new Vector2(0, 1));
        if (showing && Input.GetKeyDown(KeyCode.S)) ItemMoveSelectedCoord(new Vector2(0,-1));
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (showing) CloseBackpack();
            else SetUpCharms();
        }
        
    }

    public void CloseBackpack()
    {
        StartCoroutine(GetComponent<UI_Displayer>().HidePanel());
    }

    public void AfterClose()
    {
        foreach (KeyValuePair<Vector2, Item> p in itemCoord)
        {
            Destroy(p.Value.uiBlock.gameObject);
            p.Value.uiBlock = null;
        }
        itemCoord = new Dictionary<Vector2, Item>();
        showing = false;
        equipmentDescriptionContainer.gameObject.SetActive(false);
        charmDescriptionContainer.gameObject.SetActive(false);
    }

    public void SetUpCharms()
    {
        CloseBackpack();
        showing = true;
        equipmentDescriptionContainer.gameObject.SetActive(true);
        charmDescriptionContainer.gameObject.SetActive(true);
        int x = 0;
        int y = 0;
        int itemSlotCellSize = 120;
        itemCoord = new Dictionary<Vector2, Item>();
        foreach (Item c in Inventory.i.MyCharms)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            c.uiBlock = itemSlotRectTransform;
            c.uiBlock.Find("Image").GetComponent<Image>().sprite = c.sprite;
            c.uiBlock.gameObject.SetActive(true);
            c.uiBlock.anchoredPosition += new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            itemCoord.Add(new Vector2(x, y), c);
            x++;
            if (x >= 4)
            {
                x = 0;
                y--;
            }
        }
        selectedCoord = new Vector2(0, 0);
        ItemMoveSelectedCoord(new Vector2(0, 0));
        StartCoroutine(GetComponent<UI_Displayer>().ShowPanel());
    }

    void ItemMoveSelectedCoord(Vector2 delta)
    {
        if (!itemCoord.ContainsKey(delta + selectedCoord)) return;
        itemCoord[selectedCoord].uiBlock.Find("WhenSelected").gameObject.SetActive(false);
        selectedCoord += delta;
        itemCoord[selectedCoord].uiBlock.Find("WhenSelected").gameObject.SetActive(true);
        equipmentDCName.GetComponent<TMP_Text>().text = itemCoord[selectedCoord].displayName;
        equipmentDCDescription.GetComponent<TMP_Text>().text = itemCoord[selectedCoord].description;
    }
}
