using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float maxBlood;
    [SerializeField] private float currentBlood = 0;
    [SerializeField] private float bloodDropValue = 1;
    [SerializeField] private Image mask;
    [Header("Node sprite stuff")]
    public List<Image> nodes;
    [SerializeField] private Sprite unlitSprite;
    [SerializeField] private Sprite litSprite;
    private int maxBloodLitNodeCount = 0; //helper var for audio
    [SerializeField] private FMODUnity.EventReference passingNodeAudio;
    [SerializeField] private Cookbook cookbook;

    private void Update()
    {
        GetcurrentBloodFill();
    }
    private void GetcurrentBloodFill()
    {
        float fillAmt = currentBlood / maxBlood;
        mask.fillAmount = fillAmt;

    }


    // Call this method with the desired percentage and the total number of nodes
    private void UpdateNodesVisuals(float percentage)
    {
        int litNodeCount = CalculateLitNodeCount(percentage);

        if (litNodeCount > maxBloodLitNodeCount)
        {
            PlayPassingNodeAudio();
            maxBloodLitNodeCount = litNodeCount;
            cookbook.IncrementNodesUnlocked();
        }

        // Update the sprites based on the calculated lit node count
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].sprite = (i < litNodeCount) ? litSprite : unlitSprite;
        }
    }
    private int CalculateLitNodeCount(float percentage)
    {
        if (percentage >= 1.0f) return 5;
        if (percentage > 0.8f) return 4;
        if (percentage > 0.6f) return 3;
        if (percentage > 0.4f) return 2;
        if (percentage > 0.2f) return 1;

        return 0;
    }

    public void Increase()
    {
        currentBlood += bloodDropValue;

        if (currentBlood > maxBlood)
            currentBlood = maxBlood;
        float fillAmt = currentBlood / maxBlood;
        UpdateNodesVisuals(fillAmt);
    }

    public float GetcurrentBloodBarAmt()
    {
        return currentBlood;
    }
    public void Decrease(float amt)
    {
        currentBlood -= amt;
    }

    private void PlayPassingNodeAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(passingNodeAudio, transform.position);
    }
    public void ResetBar() {
        currentBlood = 0;
    }
}
