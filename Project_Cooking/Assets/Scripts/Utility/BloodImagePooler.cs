using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodImagePooler : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform canvasTransform;
    private float widthBuffer = 9.5f;
    [SerializeField][Range(0.15f, 0.6f)] private float spawnInterval = 0.4f;

    private List<GameObject> pooledImages = new List<GameObject>();
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] [Range(5, 89)] private int chanceOfDefaultSprite;
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
            Image imageComp = image.GetComponent<Image>();
            SetImage(imageComp);

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

    private void SetImage(Image imageComp)
    {
        int RaeZero = 0;
        int RaeNintyNine = 99;
        var rand1 = Random.Range(RaeZero, RaeNintyNine);
        if(rand1 > chanceOfDefaultSprite)
        {
            //choose one of the special sprites 
            imageComp.sprite = sprites[Random.Range(1, sprites.Count)];
        }
        else
        {
            //do default blood orb sprite 
            imageComp.sprite = sprites[0];
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
    public void KillAllObjects()
    {
        foreach (var img in pooledImages)
        {
            var anim = img.GetComponent<BloodDropFallAnim>();
            anim.KillTween();
        }
    }

    public void ReturnToPool(GameObject imageToReturn)
    {
        pooledImages.Add(imageToReturn);
        imageToReturn.SetActive(false);
    }
}
