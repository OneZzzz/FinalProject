using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameObjectPool : MonoBehaviour
{

    public static GameObjectPool instace;
    private void Awake()
    {
        if (instace == null)
            instace = this;
    }
    private void Start()
    {
        Init();
    }

    private Dictionary<string, List<GameObject>> cache;

    private Dictionary<string, Transform> parents;
    public void Init()
    {
        cache = new Dictionary<string, List<GameObject>>();
        parents = new Dictionary<string, Transform>();
    }

    public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rota)
    {
        GameObject go = FindUsableObject(key);
        if (go == null)
        {
            go = AddObject(key, prefab);
        }
        UseObject(pos, rota, go);
        return go;
    }


    public GameObject CreateObject(string parentName, string key, GameObject prefab, Vector3 pos, Quaternion rota)
    {
        GameObject go = FindUsableObject(key);
        if (go == null)
        {

            go = AddObject(parentName, key, prefab);
        }
        UseObject(pos, rota, go);
        return go;
    }


    private void UseObject(Vector3 pos, Quaternion rota, GameObject go)
    {
        go.transform.position = pos;
        go.transform.rotation = rota;
        go.SetActive(true);
    }



    private GameObject AddObject(string key, GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform);
        if (!cache.ContainsKey(key))
            cache.Add(key, new List<GameObject>());
        cache[key].Add(go);
        return go;
    }

    private GameObject AddObject(string parentName, string key, GameObject prefab)
    {
        Transform parent;
        if (!parents.ContainsKey(parentName))
        {
            GameObject pare = new GameObject(parentName);
            parent = pare.transform;
            parent.SetParent(transform);
            parents.Add(parentName, parent);
        }
        else
        {
            parent = parents[parentName];
        }

        GameObject go = Instantiate(prefab, parent);
        if (!cache.ContainsKey(key))
            cache.Add(key, new List<GameObject>());
        cache[key].Add(go);
        return go;
    }

    private GameObject FindUsableObject(string key)
    {
        if (cache.ContainsKey(key))
            return cache[key].Find(s => !s.activeSelf);
        return null;
    }


    public void CollectObject(GameObject go)
    {
        go.SetActive(false);
    }
 
    public void CollectObject(GameObject go, float delay)
    {
        StartCoroutine(CollectObjectIE(go, delay));
    }

    private IEnumerator CollectObjectIE(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }



}

