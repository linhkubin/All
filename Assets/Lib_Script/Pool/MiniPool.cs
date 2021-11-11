using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPool<T>
{
    private Stack<GameObject> stacks = new Stack<GameObject>();
    private Dictionary<GameObject,T> dict = new Dictionary<GameObject,T>();
    GameObject prefab;
    Transform parent;

    public void OnInit(GameObject prefab, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public T Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject go = null;

        if (stacks.Count <= 0)
        {
            go = GameObject.Instantiate(prefab, parent);
            dict.Add(go, go.GetComponent<T>());
        }
        else
        {
            go = stacks.Pop();
            go.SetActive(true);
        }

        go.transform.SetPositionAndRotation(pos, rot);

        return dict[go];
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
        stacks.Push(go);
    }

    public void Collect()
    {
        stacks.Clear();

        foreach (var item in dict)
        {
            if (item.Key.activeInHierarchy)
            {
                item.Key.SetActive(false);
                stacks.Push(item.Key);
            }
           
        }
    }

    public void Release()
    {
        foreach (var item in dict)
        {
            GameObject.Destroy(item.Key);
        }

        stacks.Clear();
        dict.Clear();
    }

}
