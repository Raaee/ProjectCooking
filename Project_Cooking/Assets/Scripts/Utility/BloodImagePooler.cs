using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodImagePooler : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform canvasTransform;

    [SerializeField] private float spawnInterval = 0.5f;

    private List<GameObject> pooledImages = new List<GameObject>();


    void Start()
    {
       
        StartCoroutine(SpawnImages());
    }

    IEnumerator SpawnImages()
    {
        while (true)
        {
            Debug.Log("Raeus Code check for infinite loop");
            GameObject image = GetPooledObject();
            if (image == null)
            {
                // Pool is empty, instantiate a new object
                image = Instantiate(imagePrefab, canvasTransform);
            }

            // Position the image randomly within the canvas bounds
            float randomX = Random.Range(canvasTransform.position.x - canvasTransform.localScale.x / 2,
                                         canvasTransform.position.x + canvasTransform.localScale.x / 2);
            float randomY = canvasTransform.position.y + canvasTransform.localScale.y; // Start above the canvas
            image.transform.position = new Vector3(randomX, randomY, 0);

            image.gameObject.SetActive(true);

            // Start the falling animation
            var anim = image.GetComponent<BloodDropFallAnim>();
           if(anim)
            {
                anim.StartFalling();

            }   
           else
            {
                Debug.Log("null baby");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledImages.Count; i++)
        {
            if (!pooledImages[i].activeInHierarchy)
            {
                return pooledImages[i];
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject imageToReturn)
    {
        pooledImages.Add(imageToReturn);
        imageToReturn.SetActive(false);
    }
}
