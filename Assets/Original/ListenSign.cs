using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenSign : MonoBehaviour
{
    [SerializeField] int textTag = 0;
    List<List<string>> allText = new List<List<string>>()
    {
        new List<string>(){//textTag 0
        "This is textTag 0 line 1. This is textTag 0 line 1. This is textTag 0 line 1.",
        "This is textTag 0 line 2. This is textTag 0 line 2. This is textTag 0 line 2.",
        "This is textTag 0 line 3. This is textTag 0 line 3. This is textTag 0 line 3.",
        "This is textTag 0 line 4. This is textTag 0 line 4. This is textTag 0 line 4."},
        new List<string>(){//textTag 1
        "This is textTag 1 line 1. This is textTag 1 line 1. This is textTag 3 line 1.",
        "This is textTag 1 line 2. This is textTag 1 line 2. This is textTag 3 line 2.",
        "This is textTag 1 line 3. This is textTag 1 line 3. This is textTag 3 line 3.",
        "This is textTag 1 line 4. This is textTag 1 line 4. This is textTag 3 line 4."},
        new List<string>(){//textTag 2
        "This is textTag 2 line 1. This is textTag 2 line 1. This is textTag 3 line 1.",
        "This is textTag 2 line 2. This is textTag 2 line 2. This is textTag 3 line 2.",
        "This is textTag 2 line 3. This is textTag 2 line 3. This is textTag 3 line 3.",
        "This is textTag 2 line 4. This is textTag 2 line 4. This is textTag 3 line 4."},
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
        if (Input.GetKeyDown(KeyCode.W)&&canQue)
        {
            UI_Dialogue.i.QueDialogue(allText[textTag]);
        }
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
