using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    private Transform checkPoint;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    private GameObject fx;

    public float speed=5, jumpSpeed=600,jumpSecSpeed;

    private void Start()
    {
        checkPoint = transform.Find("checkPoint");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        fx = Resources.Load<GameObject>("fx");
    }
    private void Update()
    {
        Move();
        JumpDefaul();
        JumpAttackSec();
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

}
