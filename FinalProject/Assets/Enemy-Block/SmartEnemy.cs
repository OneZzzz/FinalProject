using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    [SerializeField] bool doRoam;
    Transform playerTf;
    Rigidbody2D myRb;
    float detectDistance = 4, quitDistance = 8;
    float roamSpeed = 4f, chaseSpeed = 7f, retreatSpeed = 3f;
    float retreatTime = 1.5f;
    List<Transform> roamPosList = new List<Transform>();
    int roamIndex;
    Vector3 roamTarget;

    SpriteRenderer sp;

    enum State {Idle, Roaming, Chasing, Retreating }
    State state;
    State normalState;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        if (doRoam)
        {
            normalState = State.Roaming;
            foreach (Transform t in transform) roamPosList.Add(t);
            roamIndex = 0;
            roamTarget = roamPosList[roamIndex].position+ new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        else
        {
            normalState = State.Idle;
        }

        state = normalState;

        playerTf = FindObjectOfType<PlayControl>().gameObject.transform;
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(state == normalState)
        {
            //change to State.Chasing
            if (CheckDistance() <= detectDistance)
            {
                state = State.Chasing;
                print("enter State.Chasing");
            }
            //two cases
            if(state == State.Idle)
            {

            }
            if(state == State.Roaming)
            {
                if ((roamTarget - transform.position).magnitude <= 0.05) newRomTarget();
                if (roamTarget.x < transform.position.x)
                    sp.flipX = false;
                else
                    sp.flipX = true;
                myRb.MovePosition(transform.position + (roamTarget - transform.position).normalized * roamSpeed * Time.deltaTime);
            }
        }
        else if(state == State.Chasing)
        {
            //State.Chasing
            myRb.MovePosition(transform.position + CheckDirection() * chaseSpeed *Time.deltaTime);

            if (roamTarget.x < transform.position.x)
                sp.flipX = false;
            else
                sp.flipX = true;

            //change to normalState
            if (CheckDistance() >= quitDistance)
            {
                state = normalState;
                print("enter normalState");
            }
        }
        else if(state == State.Retreating)
        {

        }
    }

    float CheckDistance()
    {
        return (playerTf.position-transform.position).magnitude;
    }
    Vector3 CheckDirection()
    {

        return (playerTf.position - transform.position).normalized;
    }
    void newRomTarget()
    {
        int index = roamIndex;
        while(index == roamIndex)
        {
            index = (int)Random.Range(0f,roamPosList.Count-0.001f);
        }
        roamIndex = index;
        roamTarget = roamPosList[roamIndex].position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Deal Damage
        if(collision.gameObject.tag == "Player")
        {
            if (state == State.Retreating) StopAllCoroutines();
            StartCoroutine(Retreat());
            collision.gameObject.GetComponent<PlayControl>().BeAttack(5);
        }
    }

    IEnumerator Retreat()
    {
        state = State.Retreating;
        float t = 0;
        while (t < retreatTime)
        {
            t += Time.deltaTime;
            myRb.MovePosition(transform.position - new Vector3(CheckDirection().x, CheckDirection().y * Random.Range(1.4f,2f) , CheckDirection().z) * retreatSpeed * Time.deltaTime);
            yield return null;
        }
        state = normalState;
    }



}
