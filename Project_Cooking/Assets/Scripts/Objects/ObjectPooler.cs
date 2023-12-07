using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour   {

    public List<GameObject> pooledObjects;
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int pooledAmt;
    private bool willGrow = true;

    private void Start() {
        Init();
    }

    private void Init() {
        for (int i = 0; i < pooledAmt; i++) {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    
    public GameObject GetPooledObject() {
        foreach (GameObject go in pooledObjects) {
            if (!go.activeInHierarchy) {
                return go;
            }
        }
        if (willGrow) {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetPrefab() {
        return pooledObject;
    }
}
