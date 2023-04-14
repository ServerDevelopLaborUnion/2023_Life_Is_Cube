using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();

    private Transform parent = null;
    private T prefab = null;

    public Pool(T prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public T Pop()
    {
        T obj = null;

        if(pool.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }

        return obj;
    }

    public void Push(T obj)
    {
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(false);

        pool.Push(obj);
    }
}
