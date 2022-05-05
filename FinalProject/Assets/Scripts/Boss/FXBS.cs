using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXBS : MonoBehaviour
{
    public float attack = 10;
    public void Event()
    {
        RaycastHit2D[] raycastHit2D= Physics2D.RaycastAll(transform.position, Vector2.down,3.2f);
        if (raycastHit2D == null || raycastHit2D.Length <= 0) return;
        for (int i = 0; i < raycastHit2D.Length; i++)
        {
            if (raycastHit2D[i].collider.gameObject.tag == "Player")
                raycastHit2D[i].collider.gameObject.GetComponent<PlayControl>().BeAttack(attack);
        }
            
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
