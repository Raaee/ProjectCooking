using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] [Range(0.5f,1f)] private float fadeDuration = 0.75f;
    [SerializeField] [Range(0.5f, 3f)] private float endOfFrameDuration = 0.75f;
    private bool isFading = false;
    [Header("DEBUG")]
    [SerializeField] private bool fadeInOnAwake = true;
    public static FadeManager instance { get; private set; }

    private void Awake()
    {
        Init();
        if (fadeInOnAwake)
         StartCoroutine(FadeIn());
    }
    public void FadeOutAndLoadScene(int sceneIndex)
    {
        Debug.Log("going along twice? index " + sceneIndex);
        if (isFading || sceneIndex == -2)
            return;
        isFading = true;
        StartCoroutine(FadeOutAndFadeIn(sceneIndex));
    }
 

    private IEnumerator FadeOutAndFadeIn(int sceneIndex)
    {
        float elaspedTime = 0f;
        while(elaspedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elaspedTime / fadeDuration);
            elaspedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fadeCanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(endOfFrameDuration);

        if (sceneIndex == -1)
        {
            LevelManager lvlManager = FindObjectOfType<LevelManager>();
            if (lvlManager)
            {
               
                lvlManager.InvokeOnAreaChange();
            }
                
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }

        isFading = false;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fadeCanvasGroup.alpha = 0f;
    }

    private void Init()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
