using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    idle,
    attackF,
    Found
}

public class BossContrl : Boss
{
    public GameObject bossHp, finalListen;
    RectTransform hp;
    private Animator anim;
    private PlayControl player;

    private State state;

    private GameObject fxbs;

    private float attackTime, idleTime,idleMax=2,attackMax=1, maxHP = 500;

    private void Start()
    {
        currentHp = maxHp;
        hp = GameObject.FindGameObjectWithTag("ScalerBoss").GetComponent<RectTransform>();
        hp.localScale = new Vector3(1, 1, 1);
        state = State.idle;
        fxbs = Resources.Load<GameObject>("fxbs");
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindObjectOfType<PlayControl>();
        InvokeRepeating("AttackSpe", 5, 7);
    }
    private void Update()
    {
        AI();
    }
    private void AI()
    {
        if (CheckBossAnimationName("Death")) return;
        if (CheckBossAnimationName("Cast 1")) return;
        if (state==State.idle)
        {
            idleTime += Time.deltaTime;
            if (idleTime >= idleMax)
            {
                state = State.Found;
                attackTime = 0;
            }
            anim.SetBool("run", false);
        }
        else if(state==State.Found)
        {
            LookPlayer();
            if (Vector2.Distance(transform.position, player.transform.position) < 3f)
            {
                state = State.attackF;
                attackTime = 0;
            }
            else
            {
                anim.SetBool("run", true);
                float dir = (transform.localScale.x > 0) ? -1 : 1;
                transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
            }
        }
        else if (state == State.attackF)
        {
            anim.SetBool("run", false);
            attackTime += Time.deltaTime;
            if (attackTime >= attackMax)
            {
                anim.SetTrigger("attack");
                idleTime = 0;
                state = State.idle;
                Invoke("AttackGen", 0.2f);
            }
        }

    }
    private void LookPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    public void AttackGen()
    {
        float dir = (transform.localScale.x > 0) ? -1 : 1;
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(transform.position, Vector2.right * dir, 3f);
        if (raycastHit2D == null || raycastHit2D.Length <= 0) return;
        for (int i = 0; i < raycastHit2D.Length; i++)
        {
            if (raycastHit2D[i].collider.gameObject.tag == "Player")
                raycastHit2D[i].collider.gameObject.GetComponent<PlayControl>().BeAttack(attack);
        }

    }

    public void AttackSpe()
    {
        anim.SetTrigger("cast");
        Invoke("CreatFxbs", 0.5f);
    }
    public void CreatFxbs()
    {
        float y = transform.position.y + 3.2f;
        float z = transform.position.z;
        for (float i = 0; i < 10; i++)
        {
            float x = transform.position.x - 15 + i * 3.5f;
            Vector3 creatPoint = new Vector3(x, y, z);
            GameObjectPool.instace.CreateObject("FXBS", "fxbs", fxbs, creatPoint, Quaternion.identity);
        }
    }

    private void MoveDown()
    {
        if (CheckBossAnimationName("Death")) return;
        float x = player.GetPlayBack(3f);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        LookPlayer();
    }

    public override void BeAttack(float attack)
    {
        if (CheckBossAnimationName("Death")) return;
        if (!CheckBossAnimationName("Cast 1"))
        {
            anim.SetTrigger("hurt");
            Invoke("MoveDown", 0.6f);
        }
        currentHp -= attack;
        print(currentHp);
        hp.localScale = new Vector3(currentHp / maxHp, 1, 1);
        if (currentHp <= 0)
            Death();
    }
    public override void Death()
    {

        anim.SetTrigger("death");
        SoundManager.PlaySound("dielong");
        Destroy(bossHp);
        Destroy(gameObject, 1f);
        GameObject a = Instantiate(finalListen);
    }






    private bool CheckBossAnimationName(string animationName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }


}
