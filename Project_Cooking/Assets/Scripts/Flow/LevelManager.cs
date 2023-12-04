using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> baseIngredients = new List<GameObject>();
    [SerializeField] private GameObject upperCornerFLoor;
    [SerializeField] private GameObject lowerCornerFLoor;
    void Start()
    {
        SpawnAllBaseIngredients();
    }
    public void SpawnAllBaseIngredients()
    {
        foreach (GameObject go in baseIngredients)
        {
            float x = Random.Range(upperCornerFLoor.transform.position.x, lowerCornerFLoor.transform.position.x);
            float y = Random.Range(lowerCornerFLoor.transform.position.y, upperCornerFLoor.transform.position.y);
            float z = 0;
            Vector3 randomPos = new Vector3(x, y, z);
            Instantiate(go, randomPos, Quaternion.identity);
        }

    }

}
