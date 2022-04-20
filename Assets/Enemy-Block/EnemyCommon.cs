using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected bool cycle = false;
    [SerializeField] protected float damage = 0;
    [SerializeField] protected float speed = 2;

    protected Rigidbody2D rb;
    protected Pathpoint pp;

    protected bool moving;
    float hitIntervalTime = 0.5f;
    bool couldHit = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<Pathpoint>()) pp = GetComponent<Pathpoint>();
    }
    void Start()
    {
        moving = true;
        if (pp != null)
        {
            transform.position = pp.CurrentPoint();
            pp.setCycle(cycle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //damage player
            couldHit = false;
            StartCoroutine(HitInterval());
        }
    }

    IEnumerator HitInterval()
    {
        float t = 0;
        while (t<hitIntervalTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        couldHit = true;
    }
}
