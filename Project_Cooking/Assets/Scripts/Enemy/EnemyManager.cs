using UnityEngine;
/// <summary>
/// refactor into object pooling (pair program with raeus)
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;


    public GameObject SpawnEnemyAtPoint(Vector2 placement)
    {
        GameObject createdEnemy = Instantiate(enemyPrefab, placement, Quaternion.identity);

        return createdEnemy;
    }
}
