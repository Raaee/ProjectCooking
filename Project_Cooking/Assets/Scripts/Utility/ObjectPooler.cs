using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject pooledObj;
    public List<GameObject> pooledObjects;
    [SerializeField] private int intialPooledAmt = 5;
    private bool willGrow = true;

    private void Start()
    {
        Initialize();
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject go in pooledObjects)
        {
            if (!go.activeInHierarchy)
                return go;
        }
        if (willGrow)
        {
            return CreatePooledObj();
        }

        return null;
    }

    private void Initialize()
    {
        for(int i = 0; i < intialPooledAmt; i++ )
        {
            CreatePooledObj();
        }
    }
 
    private GameObject CreatePooledObj()
    {
        GameObject go = Instantiate(pooledObj);
        go.SetActive(false);
        go.transform.parent = this.transform;
        pooledObjects.Add(go);
        return go;
    }
    public GameObject GetPrefab() {
        return pooledObj;
    }
}
