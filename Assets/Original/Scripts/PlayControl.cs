using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour
{
    [SerializeField] UI_Displayer hitEffect;
    private Transform checkPoint,left,right;
    private Animator animator;
    private Rigidbody2D rigidbody;
    public SpriteRenderer sprite;

    bool canSecondJump = true;

    private RectTransform hp;

    private float maxHp = 85, currentHp;

    private GameObject fx;

    public float speed=5, jumpSpeed=600,jumpSecSpeed;

    public bool freeze = false;

    public GameObject Sword;

    private void Start()
    {
        checkPoint = transform.Find("checkPoint");
        left = transform.Find("right");
        right = transform.Find("left");
        hp = GameObject.FindGameObjectWithTag("Scaler").GetComponent<RectTransform>();
        currentHp = maxHp;
        hp.localScale = new Vector3(1,1,1);
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
        if (CheckInGround()) canSecondJump = true;
        JumpAttackSec();
            Attack3();
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
        if(CheckCharm("CharmA")) rigidbody.AddForce(Vector2.up * jumpSpeed * 1.15f);
        else rigidbody.AddForce(Vector2.up * jumpSpeed * 1f);
        //canSecondJump = true;
        print("default");
    }
    void JumpAttackSec()
    {
        if (!CheckCharm("CharmB")) return;
        if (CheckInGround()) return;
        if (!canSecondJump) return;
        //if (!CheckInGround(3)) return;
        //if (!CheckPlayAnimationName("jumpdown")) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * jumpSecSpeed);
            canSecondJump = false;
            print("sec");
        }

    }

    public float GetPlayBack(float dis)
    {
        if (!sprite.flipX)
            return transform.position.x - dis;
        else
            return transform.position.x + dis;
    }

    private bool CheckInGround(float checkDistance=0.2f)
    {
        RaycastHit2D raycastHit2D= Physics2D.Raycast(checkPoint.position,Vector2.down,checkDistance);
        if (!raycastHit2D) return false;
        if (raycastHit2D.collider.gameObject.CompareTag("ground")) return true;
        else return false;
    }
    private List<GameObject> CheckAttackPoint(Transform dir,string tag, bool IsRight)
    {
        //Vector2 temp_direction = Vector2.left;
        //if (IsRight)
        //{
        //    temp_direction = Vector2.right;
        //}
        RaycastHit2D[] raycastHit2Ds= Physics2D.CircleCastAll(dir.position, 0.2f, Vector2.up, 0.6f);
        List<GameObject> targets=new List<GameObject>();
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            if (raycastHit2Ds[i].collider.gameObject.tag == tag)
            {
                targets.Add(raycastHit2Ds[i].collider.gameObject);
            }
            Debug.Log(targets.Count);
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
            enemies = CheckAttackPoint(right, "enemy", true);
            CreatFX(right.position);
        }
        else
        {
            enemies = CheckAttackPoint(left, "enemy", false);
            CreatFX(left.position);
        }
        if (enemies == null || enemies.Count <= 0) return;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
    }
    void Attack2()
    {
        if (!Input.GetKeyDown(KeyCode.J)) return;
        if (!CheckInGround() && CheckInGround(2)) return;
        List<GameObject> enemies;
        if (sprite.flipX)
        {
            Sword.transform.localPosition = new Vector3(0.5f, -1.36f, 0);
            Quaternion Temp_angle = Quaternion.identity;
            Temp_angle.eulerAngles = Vector3.zero;
            Sword.transform.localRotation = Temp_angle;
            enemies = CheckAttackPoint(right, "enemy",true);
        }
        else
        {
            Sword.transform.localPosition = new Vector3(-0.6f, -1.36f, 0);
            Quaternion Temp_angle = Quaternion.identity;
            Temp_angle.eulerAngles = new Vector3(0,180,0);
            Sword.transform.localRotation = Temp_angle;
            enemies = CheckAttackPoint(left, "enemy",false);
        }
        if (enemies == null || enemies.Count <= 0) return;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
    }

    void Attack3()
    {
        if (!Input.GetKeyDown(KeyCode.J)) return;
        if (sprite.flipX)
        {
            Sword.transform.localPosition = new Vector3(0.5f, -1.36f, 0);
            Quaternion Temp_angle = Quaternion.identity;
            Temp_angle.eulerAngles = Vector3.zero;
            Sword.transform.localRotation = Temp_angle;
        }
        else
        {
            Sword.transform.localPosition = new Vector3(-0.6f, -1.36f, 0);
            Quaternion Temp_angle = Quaternion.identity;
            Temp_angle.eulerAngles = new Vector3(0, 180, 0);
            Sword.transform.localRotation = Temp_angle;
        }
    }
        public void BeAttack(float num)
    {
        hitEffect.Blink();
        currentHp -= num;
        if (currentHp < 0) currentHp = 0;
        if (currentHp == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp.localScale = new Vector3(currentHp / maxHp, 1, 1);
    }

    public void RecoverHp()
    {
        currentHp += 5;
        hp.localScale = new Vector3(currentHp / maxHp, 1, 1);
    }

    bool CheckCharm(string cName)
    {
        foreach(Charm c in Inventory.i.MyCharms)
        {
            if (c.itemName == cName) return true;
        }
        return false;
    }
}
