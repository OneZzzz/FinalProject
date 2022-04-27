using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimation : MonoBehaviour
{

    int clickCount = 0;
    float timer;
    public float timer2 = 0;
    public int keyPressCount = 0;
    public bool PressFast;
    bool isTimer;
    public float term;
    public Animator animSword, animRange;
    private void Update()
    {
        Attack();
        RangeAttack();
    }
    void Attack()
    {

        if (isTimer)
        {
            timer += Time.deltaTime;
        }
            if (Input.GetKeyDown(KeyCode.J))
            {
                switch (clickCount)
                {

                    case 0:

                    animSword.SetTrigger("Attack1");

                        isTimer = true;

                        clickCount++;
                        timer = 0;
                        break;


                    case 1:

                        if (timer <= term)
                        {
                        animSword.SetTrigger("Attack2");

                            clickCount++;
                        }


                        else
                        {
                        animSword.SetTrigger("Attack1");

                            clickCount = 1;
                        }


                        timer = 0;
                        break;


                    case 2:

                        if (timer <= term)
                        {
                        animSword.SetTrigger("Attack3");

                            clickCount++;
                        }


                        else
                        {
                        animSword.SetTrigger("Attack1");

                            clickCount = 1;
                        }

                        timer = 0;
                        break;


                    case 3:

                        if (timer <= term)
                        {
                        animSword.SetTrigger("Attack4");

                            clickCount = 0;

                            isTimer = false;
                        }


                        else
                        {
                        animSword.SetTrigger("Attack1");

                            clickCount = 1;
                        }

                        timer = 0;
                        break;
                }
            }
        
    }

    void RangeAttack()
    {
        if (animRange.gameObject.activeInHierarchy == true)
        {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animRange.SetBool("IsPreparing", true);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            animRange.SetBool("IsPreparing", false);
        }
        }
    }
}
