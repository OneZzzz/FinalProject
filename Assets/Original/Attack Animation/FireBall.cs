using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float MySpeed, speedIncrement, c;
    public bool Isleft;
    // Update is called once per frame
    void Update()
    {
        speedIncrement += c;
        if (Isleft)
        {
            transform.position -= new Vector3((MySpeed + speedIncrement) * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3((MySpeed + speedIncrement) * Time.deltaTime, 0, 0);
        }
    }
}
