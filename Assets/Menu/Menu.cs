using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite sp1, sp2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {
        sr.sprite = sp2;
    }
    void OnMouseExit()
    {
        sr.sprite = sp1;
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene("scene02");
    }
}
