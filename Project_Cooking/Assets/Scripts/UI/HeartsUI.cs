using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO: add affects before they dissapear
/// //make hearts amount be dynamic based on heart component 
/// </summary>
public class HeartsUI : MonoBehaviour
{
    public List<GameObject> heartSlots;
   // private int index = 0;
    //private int max = 0;
    [SerializeField] private Health health;
    private void Awake()
    {
        health.OnHurt.AddListener(RemoveHeart);
    }

    public void RemoveHeart()
    {
        if (heartSlots.Count <= 0)
            return;

        heartSlots[heartSlots.Count - 1].gameObject.SetActive(false);
        heartSlots.Remove(heartSlots[heartSlots.Count - 1]);
    }
}
