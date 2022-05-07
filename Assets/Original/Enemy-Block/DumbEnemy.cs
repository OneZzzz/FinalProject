using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : EnemyCommon
{

    private void Update()
    {
        if (moving)
        {
            rb.velocity = ((pp.NextPoint() - pp.CurrentPoint()).normalized) * speed;
            
            if (rb.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
            if (rb.velocity.x >0) GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnDestroy()
    {
        DropCoins.i.Drop((int)Random.Range(3f,4.99f), transform.position);
    }
}
