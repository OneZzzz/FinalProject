using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy")
        {
            if (other.GetComponent<BossContrl>() != null) other.GetComponent<BossContrl>().BeAttack(7);
            if (other.GetComponent<DumbEnemy>()) other.GetComponent<DumbEnemy>().DropHealth(1);
            if (other.GetComponent<SmartEnemy>()) other.GetComponent<SmartEnemy>().DropHealth(1);

            //Debug.Log("Die!");
            //Destroy(other.gameObject);
        }
    }
}
