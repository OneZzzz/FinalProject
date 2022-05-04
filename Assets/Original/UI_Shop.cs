using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    Transform ItemSlotContainer, Description, ItemSlotTemplate;
    List<string> TextInThisShop = new List<string>() { "CharmA", "CharmB", "CharmC", "CharmD", "CharmE" };
    List<Charm> InThisShop = new List<Charm>(); 
    bool showing = false;
    bool moving = false;
    int slotSpacing = 120;
    int selected = 0;

    static UI_Shop instance;
    public static UI_Shop i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UI_Shop>();
            return instance;
        }
    }

    private void Awake()
    {
        foreach(string s in TextInThisShop)
        {
            InThisShop.Add(new Charm(s));
        }
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        Description = transform.Find("Description");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Start()
    {
        GetComponent<UI_Displayer>().afterHide = AfterErase;
    }

    public void BuyCharm()
    {
        print("button down");
        if(DropCoins.i.money> InThisShop[selected].price)
        {
            DropCoins.i.money -= InThisShop[selected].price;
            Inventory.i.Aquire(InThisShop[selected].itemName);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (showing) Erase();
            else if (!showing) SetUp();
        }
        if (showing && Input.GetKeyDown(KeyCode.W)) Move(-1);
        if (showing && Input.GetKeyDown(KeyCode.S)) Move(1);
    }

    private void Move(int toward)
    {
        if ((selected + toward) < 0) return;
        if (selected + toward > InThisShop.Count-1) return;
        if (moving) return;
        selected += toward;
        StartCoroutine(MoveSlotContainer(toward));
    }

    private void UpdateDescription()
    {
        foreach(Transform child in Description.Find("CoinContainer")){
            if (child.gameObject.name != "CoinTemplate") Destroy(child.gameObject);
        }

        Description.Find("Name").GetComponent<TMP_Text>().text = InThisShop[selected].displayName;
        Description.Find("Description").GetComponent<TMP_Text>().text = InThisShop[selected].description;

        int spaceAmount = InThisShop[selected].space;
        float xStart = 0, xDelta = 50;
        xStart = spaceAmount % 2 == 0 ? -xDelta * (spaceAmount / 2 - 0.5f) : -xDelta * ((spaceAmount - 1) / 2);
        for(int i =0; i<spaceAmount; i++)
        {
            GameObject newCoin = Instantiate(Description.Find("CoinContainer").Find("CoinTemplate"), Description.Find("CoinContainer")).gameObject;
            newCoin.SetActive(true);
            newCoin.GetComponent<RectTransform>().anchoredPosition += new Vector2(xStart + xDelta*i,0);
        }
        
    }

    IEnumerator MoveSlotContainer(int toward)
    {
        bool updateDescription = false;
        moving = true;
        float timeCount = 0f;
        float totalTime = 0.3f;
        Vector2 startVec = ItemSlotContainer.GetComponent<RectTransform>().anchoredPosition;
        Vector2 endVec = startVec + new Vector2(0, slotSpacing * toward);
        while (timeCount < totalTime)
        {
            ItemSlotContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startVec, endVec, timeCount/totalTime);
            timeCount += Time.deltaTime;
            if (timeCount / totalTime >= 0.5 && !updateDescription) {
                UpdateDescription();
                updateDescription = true;
            }
            yield return new WaitForSeconds(0);
        }
        moving = false;
    }

    public void SetUp()
    {
        int y = 0;
        showing = true;
        Description.gameObject.SetActive(true);
        foreach(Charm i in InThisShop)
        {
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate.gameObject, ItemSlotContainer).GetComponent<RectTransform>();
            i.uiBlock = itemSlotRectTransform;
            i.uiBlock.gameObject.SetActive(true);
            i.uiBlock.anchoredPosition += new Vector2(0,-slotSpacing*y);
            i.uiBlock.Find("Image").GetComponent<Image>().sprite = i.sprite;
            i.uiBlock.Find("Price").GetComponent<TMP_Text>().text = "" + i.price;

            y++;
        }
        StartCoroutine(GetComponent<UI_Displayer>().ShowPanel());
    }

    public void Erase()
    {
        if (!showing) return;
        StartCoroutine(GetComponent<UI_Displayer>().HidePanel());
    }

    public void AfterErase()
    {
        Description.gameObject.SetActive(false);
        ItemSlotContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        showing = false;
        foreach (Charm i in InThisShop)
        {
            Destroy(i.uiBlock.gameObject);
        }
        selected = 0;
    }
}
