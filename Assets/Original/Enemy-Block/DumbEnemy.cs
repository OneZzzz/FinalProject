using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : EnemyCommon
{
    public float hp = 2;
    bool canHit = true;

    private void Update()
    {
        if (moving)
        {
            rb.velocity = ((pp.NextPoint() - pp.CurrentPoint()).normalized) * speed;
            
            if (rb.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
            if (rb.velocity.x >0) GetComponent<SpriteRenderer>().flipX = true;
        }
        if (hp <= 0) Destroy(this.gameObject);
    }

    public void DropHealth(float amount)
    {
        if (!canHit) return;
        hp -= amount;
        canHit = false;
        GetComponent<BeHitEffect>().BeHit();
        StartCoroutine(resetHit());
    }

    IEnumerator resetHit()
    {
        float t = Random.Range(0,0.5f), c = 0;
        while(c<t){
            c += Time.deltaTime;
            yield return null;
        }
        canHit = true;

    }

    private void OnDestroy()
    {
        SoundManager.PlaySound("diefast");
        if (CheckCharm("CharmC")) FindObjectOfType<PlayControl>().RecoverHp();
        DropCoins.i.Drop((int)Random.Range(3f,4.99f), transform.position);
    }
    bool CheckCharm(string cName)
    {
        foreach (Charm c in Inventory.i.MyCharms)
        {
            if (c.itemName == cName) return true;
        }
        return false;
    }

}
