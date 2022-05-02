using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float MySpeed, speedIncrement, c;
    // Update is called once per frame
    void Update()
    {
        speedIncrement += c;
        transform.position += new Vector3((MySpeed+speedIncrement) * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
