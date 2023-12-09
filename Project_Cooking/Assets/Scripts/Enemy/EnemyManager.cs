using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// refactor into object pooling (pair program with raeus)
/// </summary>
public class EnemyManager : MonoBehaviour
{
    private EnemyDeathHandler enemyDeathHandler;
    public List<GameObject> enemiesInDungeon = new List<GameObject>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amtEnemiesPerRound = 3;
    [SerializeField] private float beforeSpawnDelay = 2f;

    [SerializeField] private GameObject upperSpawn;
    [SerializeField] private GameObject lowerSpawn;
    [HideInInspector]public UnityEvent OnDungeonArea;
    private ObjectPooler enemyObjectPooler;

    private void Awake()
    {
        enemyObjectPooler = GetComponent<ObjectPooler>();

    }
   

    public GameObject SpawnEnemyAtPoint(Vector2 placement)  {
        GameObject createdEnemy = Instantiate(enemyPrefab, placement, Quaternion.identity);
        enemiesInDungeon.Add(createdEnemy);

        return createdEnemy;
    }
    public void SpawnAllEnemies() {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies() {
        
        yield return new WaitForSeconds(beforeSpawnDelay);
        for (int i = 0; i < amtEnemiesPerRound; i++) {

            float x = Random.Range(upperSpawn.transform.position.x, lowerSpawn.transform.position.x);
            float y = Random.Range(lowerSpawn.transform.position.y, upperSpawn.transform.position.y);
            float z = 0;
            Vector3 randomPos = new Vector3(x, y, z);

            GameObject enemy = enemyObjectPooler.GetPooledObject(); //Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            enemy.transform.position = randomPos;
            enemy.GetComponent<EnemyStateHandler>().Init();
            enemy.GetComponent<Health>().Init();
            enemy.SetActive(true);

            enemiesInDungeon.Add(enemy);
        }
    }
}
