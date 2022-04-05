using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
[SerializeField] private PoolObject _prefab = null;
    [SerializeField] private Transform _container = null;
    [SerializeField] private int _minCopacity = 1;

    private List<PoolObject> _pool = new List<PoolObject>();

    private void Start()
    {
        _pool.Clear();
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<PoolObject>(_minCopacity);

        for (int i = 0; i < _minCopacity; i++)
        {
            CreateElement();
        }
    }

    private PoolObject CreateElement()
    {
        var createObject = Instantiate(_prefab, _container);
        createObject.gameObject.SetActive(false);
        
        _pool.Add(createObject);

        return createObject;
    }

    private bool TryGetElement(out PoolObject element)
    {
        foreach (var i in _pool)
        {
            if (!i.gameObject.activeInHierarchy)
            {
                element = i;
                i.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public PoolObject GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }
        
        return CreateElement();
    }

    public PoolObject GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    public PoolObject GetFreeElement(Transform position)
    {
        var element = GetFreeElement();
        element.transform.position = position.position;
        element.transform.rotation = position.rotation;
        return element;
    }
    
    public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }
}
