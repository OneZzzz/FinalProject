using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    [SerializeField] protected float health = 10,currentHp;
    [SerializeField] protected bool cycle = false;
    [SerializeField] protected float damage = 0;
    [SerializeField] protected float speed = 2;

    protected Rigidbody2D rb;
    protected Pathpoint pp;

    protected bool moving;
    float hitIntervalTime = 0.5f;
    bool couldHit = true;

    public float attack = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<Pathpoint>()) pp = GetComponent<Pathpoint>();
    }
    void Start()
    {
        currentHp = health;
        moving = true;
        if (pp != null)
        {
            transform.position = pp.CurrentPoint();
            pp.setCycle(cycle);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            couldHit = false;
            StartCoroutine(HitInterval());

            collision.gameObject.GetComponent<PlayControl>().BeAttack(attack);

        }
    }


    public virtual void BeAttack(float attack)
    {
        currentHp -= attack;
        if (currentHp <= 0)
            Destroy(gameObject);
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
