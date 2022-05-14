using System.Collections;
using UnityEngine;


public class Boss : MonoBehaviour
{
    [HideInInspector]
    public float maxHp = 200, currentHp=100,attack=10,speed=1.5f;

    public virtual void BeAttack(float attack)
    {
        currentHp -= attack;
        if (currentHp <= 0)
            Death();
    }
    public virtual void Death()
    {
        Destroy(gameObject);
    }
    
}
