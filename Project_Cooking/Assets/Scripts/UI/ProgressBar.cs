using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int max;
    [SerializeField] private int current;
    [SerializeField] private Image mask;

    [SerializeField] private Image fill;
    [SerializeField] private Color fillColor;


    private void Start()
    {
        fill.color = fillColor;
    }
    private void Update()
    {
        GetCurrentFill();
    }
    private void GetCurrentFill()
    {
        float fillAmt = (float)current / (float)max;
        mask.fillAmount = fillAmt;
    }

    public void increase(int amtToAdd)
    {
        current += amtToAdd;
    }
}
