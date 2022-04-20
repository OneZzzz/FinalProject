using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : EnemyCommon
{
    private void Update()
    {
        if (moving) rb.velocity = ((pp.NextPoint() - pp.CurrentPoint()).normalized) * speed;
    }
}
