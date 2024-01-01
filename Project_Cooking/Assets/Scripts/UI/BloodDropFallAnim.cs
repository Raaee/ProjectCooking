using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDropFallAnim : MonoBehaviour
{
    private float duration = 7f; // Adjust duration
    private float fallDistance = 15f; // Adjust fall distance

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartFalling();
    }

    public void StartFalling()
    {
        //add some randomness, woooooooooo
        var _duration = Random.Range(duration -2f, duration + 2f);
        var _fallDistance = Random.Range(fallDistance - 2f, fallDistance + 2f);

        if (!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        // Calculate the target position based on fall distance
        float targetY = rectTransform.position.y - _fallDistance;

        // Move the image downwards smoothly using DOTween, stopping at the target position
        rectTransform.DOMoveY(targetY, _duration).SetEase(Ease.InSine).OnComplete(() => StopFalling());
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
