using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool canCollect = false;
    bool sit = true;
    Transform playerTransform;


    IEnumerator Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f,0.5f), Random.Range(0,1f)).normalized * 300f);
        
        var body = GetComponent<Rigidbody2D>();
        var impulse = (Random.Range(-400,400)* Mathf.Deg2Rad) * body.inertia;
        body.AddTorque(impulse, ForceMode2D.Impulse);

        playerTransform = FindObjectOfType<PlayControl>().transform;
        float count = 0;
        while (count < 0.5f)
        {
            count += Time.deltaTime;
            yield return null;
        }
        canCollect = true;
    }

    void Update()
    {
        if (sit)
        {
            if(canCollect && (playerTransform.position - transform.position).magnitude <= 1.5)
            {
                sit = false;
                Destroy(GetComponent<Rigidbody2D>());
                GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }
        else
        {
            transform.position += (playerTransform.position - transform.position).normalized * 5 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DropCoins.i.money += 1;
            Destroy(gameObject);
        }
    }
}
