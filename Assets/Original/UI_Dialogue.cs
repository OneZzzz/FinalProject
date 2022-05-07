using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Dialogue : MonoBehaviour
{
    static UI_Dialogue instance;
    int currentIndex = 0;
    public bool inDialogue = false;
    bool goingToShop = false;
    TMP_Text Text;
    List<string> currentText;
    PlayControl player;

    
    public static UI_Dialogue i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UI_Dialogue>();
            return instance;
        }
    }

    private void Start()
    {
        Text = transform.Find("Text").GetComponent<TMP_Text>();
        player = FindObjectOfType<PlayControl>();
    }

    void Update()
    {
        if (!inDialogue) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentIndex + 1 < currentText.Count)
            {
                currentIndex++;
                Text.text = currentText[currentIndex];
            }
            else
            {
                CloseDialogue();
                if(goingToShop)UI_Shop.i.SetUp();
            }
        }
    }

    public void QueDialogue(List<string> text, bool toShop)
    {
        goingToShop = toShop;
        inDialogue = true;
        currentIndex = 0;
        currentText = text;
        Text.text = currentText[currentIndex];
        player.freeze = true;
        StartCoroutine(GetComponent<UI_Displayer>().ShowPanel());
    }

    void CloseDialogue()
    {
        StartCoroutine(GetComponent<UI_Displayer>().HidePanel());
        player.freeze = false;
        StartCoroutine(delayQuitDialogue());
    }

    IEnumerator delayQuitDialogue()
    {
        float t = 0;
        while (t < 0.5)
        {
            t += Time.deltaTime;
            yield return null;
        }
        inDialogue = false;
    }
}
