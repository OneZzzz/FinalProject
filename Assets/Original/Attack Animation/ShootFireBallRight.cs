using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBallRight : MonoBehaviour
{
    public GameObject FireBall;
    public GameObject Myparent;
public void Shoot()
    {
        GameObject tempFire = Instantiate(FireBall, Myparent.transform);
        tempFire.transform.parent = null;
        this.GetComponent<Animator>().SetBool("IsPreparingLeft", false);
        this.GetComponent<Animator>().SetBool("IsPreparingRight", false);
    }

    public void Shoot2()
    {
        GameObject tempFire = Instantiate(FireBall, Myparent.transform);
        tempFire.GetComponent<FireBall>().Isleft = true;
        tempFire.transform.parent = null;
        this.GetComponent<Animator>().SetBool("IsPreparingLeft", false);
        this.GetComponent<Animator>().SetBool("IsPreparingRight", false);
    }
}
