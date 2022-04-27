using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneAttack : MonoBehaviour
{
    public void DoneAttacking()
    {
        this.GetComponent<Animator>().SetBool("DoneAttack", true);
    }

    public void StartAttacking()
    {
        this.GetComponent<Animator>().SetBool("DoneAttack", false);
    }
}
