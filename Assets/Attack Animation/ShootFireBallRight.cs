using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBallRight : MonoBehaviour
{
    public GameObject FireBall;
    public GameObject Myparent;
public void Shoot()
    {
        Instantiate(FireBall,Myparent.transform);
        this.GetComponent<Animator>().SetBool("IsPreparing", false);
    }
}
