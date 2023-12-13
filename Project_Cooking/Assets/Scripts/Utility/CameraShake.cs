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
    [SerializeField] private ShakePattern DeathShakePattern = ShakePattern.Random;
    [SerializeField] private ShakePattern ScreechShakePattern = ShakePattern.SineWave;
    [SerializeField] private GameObject player;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        player.GetComponent<Health>().OnDeath.AddListener(ScreechShake);
        player.GetComponent<ScreechAbility>().OnScreenAbility.AddListener(DeathShake);
    }
    public void ScreechShake() {
        Shake(ScreechShakePattern);
    }
    public void DeathShake() {
        Shake(DeathShakePattern);
    }
    public void Shake(ShakePattern pattern)
    {
        StartCoroutine(ShakeCoroutine(pattern));
    }

    IEnumerator ShakeCoroutine(ShakePattern pattern)
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