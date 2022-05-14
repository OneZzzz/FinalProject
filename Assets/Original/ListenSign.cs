using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenSign : MonoBehaviour
{
    [SerializeField] int textTag = 0;
    [SerializeField] bool connectToShop = false;
    [SerializeField] int changeTextTag = -1;
    List<List<string>> allText = new List<List<string>>()
    {
        new List<string>(){//textTag 0
        "Ho ho ho, old Mercury haven't got any customer for ages.",
        "How are you doing young man?",
        "You want to get stronger to defeat Lord Scaridemore?",
        "Then you're definetly in the right place!",
        "Take a look!"},
        new List<string>(){//textTag 1
        "What are you doing here young man?",
        "You're really not supposed to be here... unless you want to be smashed and served as dinner to Lord Scaridemore.",
        "Oh how brave!",
        "You should visit my friend Mercury if you wanna destroy Scaridemore.",
        "He got some nice stuffs to sell you!",
        "Just head straight and you'd see him. On the lowest east end. Fill up your pocket though, he aint cheap.",
        "I wish you the best of luck..."},
        new List<string>(){//textTag 2
        "Welcome back!",
        "Old Mercury never say no to a customer!"},
        new List<string>(){//textTag 3
        "STOP!",
        "Who you are? Why you are here? What you want to do?",
        "Huh? You trynna kill the Lord?",
        "You? Don't make a fool out of yourself.",
        "I'll let you go just to see this hoplessly idiotic act",
        "Go off champ"},
        new List<string>(){//textTag 4
        "Why you still here?",
        "Ain't you planning to kill the Lord?",
        "Huh idiot."},
         new List<string>(){//textTag 5
        "Hey! You Made It!",
        "Wow congradulations!",
        "Nobody expected a little knight from Mew Kroy Ytisrevinu to accomplish something so marvelous",
         "This is truly impressive.",
         "I guess it's time to say goodbye!",
         "Thank you, for playing our game, from developer Peng, Emily, Leo, and Zeric",
         "Wish the great memory lives on!",
         "Goodbye!"},
    };

    bool canQue = false;

    RectTransform ArrowRect;

    private void Start()
    {
        ArrowRect = transform.Find("Capsule").GetComponent<RectTransform>();
        StartCoroutine(MoveArrow());
    }

    private void Update()
    {
        if (UI_Dialogue.i.inDialogue) return;
        if (Input.GetKeyDown(KeyCode.Return)&&canQue && !UI_Shop.i.showing)
        {
            Invoke("LoadText", 0.1f);
        }
    }

    void LoadText()
    {
        UI_Dialogue.i.QueDialogue(allText[textTag], connectToShop, textTag == 5);
        if (changeTextTag != -1) textTag = changeTextTag;
    }

    IEnumerator MoveArrow()
    {
        float time = 0;
        float weight = 6;
        Vector2 orgPos = ArrowRect.anchoredPosition;
        while (true)
        {
            time += Time.deltaTime*weight;
            ArrowRect.anchoredPosition = new Vector2(orgPos.x, orgPos.y+Mathf.Sin(time)/5);
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canQue = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canQue = false;
        }
    }
}
