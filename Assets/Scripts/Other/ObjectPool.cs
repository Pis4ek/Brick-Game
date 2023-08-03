using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    public T PoolPrefab { get; }
    public bool AutoExpand { get; set; } = false;
    public Transform Container { get; }

    private List<T> _pool;

    public ObjectPool(T prefab, int count = 1, Transform container = null)
    {
        PoolPrefab = prefab;
        Container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    public T GetElement()
    {
        foreach (var poolElement in _pool)
        {
            if (!poolElement.gameObject.activeInHierarchy)
            {
                poolElement.gameObject.SetActive(true);
                return poolElement;
            }
        }
        if (AutoExpand)
        {
            var element = CreateObject();
            element.gameObject.SetActive(true);
            return element;
        }
        throw new System.Exception($"ObjectPool has not free objects of type {typeof(T)}");
    }

    public bool TryGetElement(out T element)
    {
        foreach (var poolElement in _pool)
        {
            if (!poolElement.gameObject.activeInHierarchy)
            {
                element = poolElement;
                element.gameObject.SetActive(true);
                return true;
            }
        }
        if (AutoExpand)
        {
            element = CreateObject();
            element.gameObject.SetActive(true);
            return true;
        }
        element = null;
        return false;
    }

    public void HideAllElements()
    {
        foreach (var element in _pool)
        {
            if (element.gameObject.activeInHierarchy)
            {
                element.gameObject.SetActive(false);
            }
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(PoolPrefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        createdObject.name = $"ObjectPoolElement_{_pool.Count}";

        _pool.Add(createdObject);
        return createdObject;
    }
}

