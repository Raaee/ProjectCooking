using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;

public class EnemyManager : MonoBehaviour
{
   
    public List<GameObject> enemiesInDungeon = new List<GameObject>();
    [SerializeField] private ObjectPooler objPooler1;
    [SerializeField] private ObjectPooler objPooler2;
    [SerializeField] private ObjectPooler objPooler3;

    [Header("VARIABLES")]
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
    public void SpawnRandomEnemy() {
        int randomNum = Random.Range(0, 3);
        switch (randomNum) {
            case 0: 
                StartCoroutine(SpawnEnemies(objPooler1));
                break;
            case 1:
                StartCoroutine(SpawnEnemies(objPooler2));
                break;
            case 2:
                StartCoroutine(SpawnEnemies(objPooler3));
                break;
        }
    }   

    [ProButton]
    public void SpawnAllEnemies() {
        for (int i = 0; i < amtEnemiesPerRound; i++) {
            SpawnRandomEnemy();
        }
    }
    private IEnumerator SpawnEnemies(ObjectPooler pooler) {
        
        yield return new WaitForSeconds(beforeSpawnDelay);
        
        float x = Random.Range(upperSpawn.transform.position.x, lowerSpawn.transform.position.x);
        float y = Random.Range(lowerSpawn.transform.position.y, upperSpawn.transform.position.y);
        float z = 1;
        Vector3 randomPos = new Vector3(x, y, z);

        GameObject enemy = pooler.GetPooledObject();
        enemy.transform.position = randomPos;
        enemy.GetComponent<EnemyStateHandler>().Init();
        enemy.GetComponent<Health>().InitHealth();
        enemy.SetActive(true);

        enemiesInDungeon.Add(enemy);        
    }
    public GameObject SpawnEnemyAtPoint(Vector2 placement) {
        GameObject createdEnemy = Instantiate(enemyObjectPooler.GetPrefab(), placement, Quaternion.identity);
        enemiesInDungeon.Add(createdEnemy);

        return createdEnemy;
    }
}
