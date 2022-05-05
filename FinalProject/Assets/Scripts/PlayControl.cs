using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour
{
    private Transform checkPoint,left,right;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    private Image hp;

    private float maxHp = 100, currentHp;

    private float attack = 10;

    private GameObject fx,hurtPart;

    public float speed=5, jumpSpeed=600,jumpSecSpeed;

    private void Start()
    {
        checkPoint = transform.Find("checkPoint");
        left = transform.Find("left");
        right = transform.Find("right");
        hp = GameObject.FindGameObjectWithTag("hp").GetComponent<Image>();
        currentHp = maxHp;
        hp.fillAmount = currentHp / maxHp;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        fx = Resources.Load<GameObject>("fx");
        hurtPart= fx = Resources.Load<GameObject>("hurtPart");
    }
    private void Update()
    {
        Move();
        JumpDefaul();
        JumpAttackSec();
        Attack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trigger")
        {
            collision.GetComponent<BossTrigger>().CreatBoss();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        transform.Translate(x * Time.deltaTime * speed,0,0);

        if (x < 0) sprite.flipX = true;
        if (x > 0) sprite.flipX = false;

        animator.SetFloat("speedX", Mathf.Abs(x));
    }
    void JumpDefaul()
    {
        animator.SetFloat("speedY", rigidbody.velocity.y);
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!CheckInGround()) return;
        rigidbody.AddForce(Vector2.up * jumpSpeed);
    }
    void JumpAttackSec()
    {
        if (CheckInGround()) return;
        if (!CheckInGround(2)) return;
        if (CheckPlayAnimationName("jumpup")) return;
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * jumpSecSpeed);
            CreatFX(Vector2.down, 2, checkPoint, "ground");
        }

    }



    private bool CheckInGround(float checkDistance=0.2f)
    {
        RaycastHit2D raycastHit2D= Physics2D.Raycast(checkPoint.position,Vector2.down,checkDistance);
        if (!raycastHit2D) return false;
        if (raycastHit2D.collider.gameObject.CompareTag("ground")) return true;
        else return false;
    }
    private List<GameObject> CheckAttackPoint(Transform dir,string tag)
    {
        RaycastHit2D[] raycastHit2Ds= Physics2D.CircleCastAll(dir.position, 0.05f, Vector2.up);
        List<GameObject> targets=new List<GameObject>();
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            if (raycastHit2Ds[i].collider.gameObject.tag == tag)
            {
                targets.Add(raycastHit2Ds[i].collider.gameObject);
            }
        }
        return targets;
    }


    private bool CheckPlayAnimationName(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName); 
    }

    private void CreatFX(Vector2 dirction,float distance,Transform checkPoint,string tag)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(checkPoint.position,dirction, distance);
        if (!raycastHit2D) return ;
        if (!raycastHit2D.collider.gameObject.CompareTag(tag)) return;
        GameObjectPool.instace.CreateObject("FX","fx", fx, raycastHit2D.point, Quaternion.identity);
    }
    private void CreatFX(Vector2 target)
    {
        GameObjectPool.instace.CreateObject("hurt", "hurt", hurtPart,target, Quaternion.identity);
    }
    void Attack()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CheckInGround() && CheckInGround(2)  && CheckPlayAnimationName("jumpup")) return;
        if (CheckPlayAnimationName("attcak") || CheckPlayAnimationName("hurt")) return;
        animator.SetTrigger("attack");
        

    }
    public void CreatAttackFx()
    {
        List<GameObject> enemies;
        if (sprite.flipX)
        {
            enemies = CheckAttackPoint(right, "enemy");
        }
        else
        {
            enemies = CheckAttackPoint(left, "enemy");
        }
        if (enemies == null || enemies.Count <= 0) return;
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].GetComponent<EnemyCommon>()!=null)
            enemies[i].GetComponent<EnemyCommon>().BeAttack(attack);
            if (enemies[i].GetComponent<Boss>() != null)
                enemies[i].GetComponent<Boss>().BeAttack(attack);
        }
        if (sprite.flipX)
        {
            CreatFX(right.position);
        }
        else
        {
            CreatFX(left.position);
        }
    }
    public void BeAttack(float num)
    {
        if (CheckPlayAnimationName("hurt")) return;
        animator.SetTrigger("hurt");
        currentHp -= num;
        if (currentHp < 0) currentHp = 0;
        if (currentHp == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp.fillAmount = currentHp / maxHp;

    }

    public float GetPlayBack(float dis)
    {
        if (!sprite.flipX)
            return transform.position.x - dis;
        else
            return transform.position.x + dis;
    }
}
