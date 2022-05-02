using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SideScrollerBlockMovement : MonoBehaviour
{
    [SerializeField] bool damage = false;
    [SerializeField] bool rotation = false;
    [SerializeField] float rotationSpeed = 0f;
    [SerializeField] bool moveBySelf = true;
    [SerializeField] bool cycle = false;
    [SerializeField] float speed = 2;
    float zRotation = 0;
    bool move;
    Rigidbody2D rb;
    Pathpoint pp;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<Pathpoint>()) pp = GetComponent<Pathpoint>();
        move = moveBySelf;
    }
    void Start()
    {
        if (pp!=null)
        {
            transform.position = pp.CurrentPoint();
            pp.setCycle(cycle);
        }
    }

    void Update()
    {
        if (move) rb.velocity = ((pp.NextPoint() - pp.CurrentPoint()).normalized) * speed;
        if (rotation)
        {
            zRotation += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damage && collision.gameObject.tag == "Player")
        {
            //Touches player
        }
    }
}
