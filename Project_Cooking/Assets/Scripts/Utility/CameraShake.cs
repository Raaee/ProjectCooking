using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public enum ShakePattern
    {
        Random,
        SineWave,
        PerlinNoise
    }

    [SerializeField] [Range(0.01f, 0.5f)] private float duration = 0.25f;
    [SerializeField] [Range(0.01f, 0.5f)] private float magnitude = 0.25f;
    [SerializeField] private ShakePattern pattern = ShakePattern.Random;
    [SerializeField] private Health playerHealth;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        playerHealth.OnDeath.AddListener(Shake);
    }

    [ProButton]
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float xOffset, yOffset;
            switch (pattern)
            {
                case ShakePattern.Random:
                    xOffset = Random.Range(-magnitude, magnitude);
                    yOffset = Random.Range(-magnitude, magnitude);
                    break;
                case ShakePattern.SineWave:
                    xOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2 / duration) * magnitude;
                    yOffset = Mathf.Cos(elapsedTime * Mathf.PI * 2 / duration) * magnitude;
                    break;
                case ShakePattern.PerlinNoise:
                    xOffset = Mathf.PerlinNoise(elapsedTime, 0f) * magnitude;
                    yOffset = Mathf.PerlinNoise(0f, elapsedTime) * magnitude;
                    break;
                default:
                    xOffset = 0f;
                    yOffset = 0f;
                    break;
            }

            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}