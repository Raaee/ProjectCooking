using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodImagePooler : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform canvasTransform;
    private float widthBuffer = 10;
   [SerializeField][Range(0.1f, 0.6f)] private float spawnInterval = 0.4f;

    private List<GameObject> pooledImages = new List<GameObject>();


    void Start()
    {
       
        StartCoroutine(SpawnImages());
    }

    IEnumerator SpawnImages()
    {
        while (true)
        {
            GameObject image = GetPooledObject();
            if (image == null)
            {
                // Pool is empty, instantiate a new object
                image = Instantiate(imagePrefab, canvasTransform);
            }

            // Position the image randomly within the canvas bounds
            float randomX = Random.Range(canvasTransform.position.x - (canvasTransform.localScale.x / 2 + widthBuffer),
                                         canvasTransform.position.x + (canvasTransform.localScale.x / 2 + widthBuffer));
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
