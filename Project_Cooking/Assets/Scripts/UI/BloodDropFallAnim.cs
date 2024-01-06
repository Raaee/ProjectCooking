using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDropFallAnim : MonoBehaviour
{
    private float duration = 4f; // Adjust duration
    private float fallDistance = 18f; // Adjust fall distance

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartFalling();
    }

    public void StartFalling()
    { // Generate a random scale value between 0.5 and 2.0
        float randomScale = Random.Range(0.333f, 1.333f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        //add some randomness, woooooooooo
        float randBuffer = 1f;
        var _duration = Random.Range(duration - randBuffer, duration + randBuffer);
        var _fallDistance = Random.Range(fallDistance - randBuffer, fallDistance + randBuffer);

        if (!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        // Calculate the target position based on fall distance
        float targetY = rectTransform.position.y - _fallDistance;

        // Move the image downwards smoothly using DOTween, stopping at the target position
        rectTransform?.DOMoveY(targetY, _duration).SetEase(Ease.InSine).OnComplete(() => StopFalling());
    }

    void StopFalling()
    {
        BloodImagePooler bloodImagePooler = FindObjectOfType<BloodImagePooler>();
        if (!bloodImagePooler)
        {
            Debug.Log("blood image bull baby");
        }
        bloodImagePooler.ReturnToPool(gameObject);
    }
}
