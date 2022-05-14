using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitEffect : MonoBehaviour
{
    [SerializeField]AnimationCurve curve;
    float speed = 2;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void BeHit()
    {
        StopAllCoroutines();
        sr.color = new Color(1, 1, 1, 1);
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float t = 0;
        while (t < 1)
        {
            sr.color = new Color(1, curve.Evaluate(t), curve.Evaluate(t), 1);
            t += Time.deltaTime * speed;
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 1);
    }
}
