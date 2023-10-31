using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour  
{

    [SerializeField] private Renderer spriteShaderRenderer; //should be the spriterenderer?
    private Health playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.OnHurt.AddListener(Flash);
    }

    /// <summary>
    /// This will be using its shader/renderer. Might have to refactor if it affects the actual animations
    /// </summary>
    public void Flash()
    {
         Material mat = spriteShaderRenderer.material;

        if (!mat) return;
        int amtOfFlashes = 4;
        float finalFlashAmt = 0.1f;
       
        mat.DOFloat(finalFlashAmt, "_HitEffectBlend", .1f).SetEase(Ease.InOutBack).SetLoops(amtOfFlashes, LoopType.Yoyo).OnComplete(() =>
        {
            mat.SetFloat("_HitEffectBlend", 0f);
        });

    }
}
