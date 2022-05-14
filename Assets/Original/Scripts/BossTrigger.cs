using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") CreatBoss();
    }
    public void CreatBoss()
    {
        boss.SetActive(true);
        bossHp.SetActive(true);
    }
}
