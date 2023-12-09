using System.Collections.Generic;
using UnityEngine;

public class BloodDropSpriteData : MonoBehaviour
{
    public List<Sprite> normalSprites;
    public List<Sprite> highLightedSprites;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprites[Random.Range(1, 100) % 2];
    }

    public void highlightSprites()
    {
        sr.sprite = highLightedSprites[Random.Range(1, 100) % 2];
    }
}
