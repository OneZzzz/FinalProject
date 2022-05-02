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

    private GameObject fx;

    public float speed=5, jumpSpeed=600,jumpSecSpeed;

    public bool freeze = false;

    private void Start()
    {
        checkPoint = transform.Find("checkPoint");
        left = transform.Find("right");
        right = transform.Find("left");
        hp = GameObject.FindGameObjectWithTag("hp").GetComponent<Image>();
        currentHp = maxHp;
        hp.fillAmount = currentHp / maxHp;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        fx = Resources.Load<GameObject>("fx");
    }
    private void Update()
    {
        if (freeze) return;
        Move();
        JumpDefaul();
        JumpAttackSec();
        Attack();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        transform.Translate(x * Time.deltaTime * speed,0,0);

        if (x < 0) sprite.flipX = false;
        if (x > 0) sprite.flipX = true;

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
        if (!CheckPlayAnimationName("jumpdown")) return;
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
        RaycastHit2D[] raycastHit2Ds= Physics2D.CircleCastAll(dir.position, 0.2f, Vector2.up);
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
        GameObjectPool.instace.CreateObject("FX", "fx", fx,target, Quaternion.identity);
    }
    void Attack()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!CheckInGround() && CheckInGround(2)) return;
        List<GameObject> enemies;
        if (sprite.flipX)
        {
            enemies = CheckAttackPoint(right, "enemy");
            CreatFX(right.position);
        }
        else
        {
            enemies = CheckAttackPoint(left, "enemy");
            CreatFX(left.position);
        }
        if (enemies == null || enemies.Count <= 0) return;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
    }
    public void BeAttack(float num)
    {
        currentHp -= num;
        if (currentHp < 0) currentHp = 0;
        if (currentHp == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp.fillAmount = currentHp / maxHp;

    }
}
