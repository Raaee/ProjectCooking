using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int max;
    private int current = 0;
    [SerializeField] private Image mask;
    [Header("Node sprite stuff")]
    public List<Image> nodes;
    [SerializeField] private Sprite unlitSprite;
    [SerializeField] private Sprite litSprite;




    private void Start()
    {

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
   

    // Call this method with the desired percentage and the total number of nodes
    private void UpdateNodesVisuals(float percentage, int totalNodes)
    {    
        int litNodeCount = CalculateLitNodeCount(percentage);
       
        // Update the sprites based on the calculated lit node count
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].sprite = (i < litNodeCount) ? litSprite : unlitSprite;
        }
    }
    private int CalculateLitNodeCount(float percentage)
    {
        if (percentage > 0.8f) return 4;
        if (percentage > 0.6f) return 3;
        if (percentage > 0.4f) return 2;
        if (percentage > 0.2f) return 1;

        return 0;
    }

    public void Increase(int amtToAdd)
    {
        current += amtToAdd;

        if (current > max)
            current = max;
        float fillAmt = (float)current / (float)max;
        UpdateNodesVisuals(fillAmt, nodes.Count);
    }

    public int GetCurrentBarAmt()
    {
        return current;
    }
    public void Decrease(int amt)
    {
        current -= amt;
    }
}
