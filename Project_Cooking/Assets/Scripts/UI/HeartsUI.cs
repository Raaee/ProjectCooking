using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO: add affects before they dissapear
/// //make hearts amount be dynamic based on heart component 
/// </summary>
public class HeartsUI : MonoBehaviour
{
    public List<GameObject> heartSlots;

    [SerializeField] private Health health;
    [SerializeField] private Sprite emptyHeart; //kinda like raeus's heart! 
    [SerializeField] private Sprite fullHeart;
    private int heartPointer;

    private void Awake()
    {
        health.OnHurt.AddListener(RemoveHeart);
        health.OnHeal.AddListener(FillUpHeart);
    }
    private void Start()
    {
        heartPointer = health.maxHealth - 1;
    }

    public void RemoveHeart()
    {
        if (heartPointer < 0)
            return;
        //take the "last" heart and change the sprite, have a pointer to it 
        heartSlots[heartPointer].GetComponent<Image>().sprite = emptyHeart;
        heartPointer--;
    }

    public void FillUpHeart()
    {

        if (heartPointer >= health.maxHealth - 1)
            return;
        heartPointer++;

        heartSlots[heartPointer].GetComponent<Image>().sprite = fullHeart;
    }
}
