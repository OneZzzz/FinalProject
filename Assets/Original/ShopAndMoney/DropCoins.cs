using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropCoins : MonoBehaviour
{

    [SerializeField] GameObject coin;
    [SerializeField] Transform tryLocation;
    [SerializeField] TMP_Text moneyText;

    public int money = 0;

    static DropCoins instance;
    public static DropCoins i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<DropCoins>();
            return instance;
        }
    }

    void Update()
    {
        moneyText.text = ""+money;
        if (Input.GetKeyDown(KeyCode.Equals)) money += 10;
    }

    public void Drop(int amount, Vector3 where)
    {
        for(int i =0; i<amount; i++)
        {
            Instantiate(coin, where, Quaternion.identity);
        }
    }
}
