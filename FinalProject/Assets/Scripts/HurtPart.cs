using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPart : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Close", 1f);
    }
    void Close()
    {
        gameObject.SetActive(false);
    }
}
