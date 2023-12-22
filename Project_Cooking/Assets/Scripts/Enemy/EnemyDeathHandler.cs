using UnityEngine;
using UnityEngine.Events;

public class EnemyDeathHandler : MonoBehaviour
{
    private Health enemyHealth;
    [SerializeField] private GameObject bloodDropPrefab;
    [SerializeField] private int amtOfBloodDrops = 3;
    private ObjectPooler bloodObjectPooler;


    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        bloodObjectPooler = GetComponent<ObjectPooler>();
        enemyHealth.OnDeath.AddListener(OnEnemyDeath);
        enemyHealth.OnHurt.AddListener(OnEnemyHurt);
    }
    public void OnEnemyHurt()
    {


    }

    public void OnEnemyDeath()
    {
        //very bad code 
        var dungeonMusic = FindObjectOfType<DungeonMusic>();
        if (dungeonMusic)
            dungeonMusic.IncreaseEnemyKilled();
        //enemmy death sound 
        //visual feedbacl
        //spawn x amount of droplets around me
        for (int i = 0; i < amtOfBloodDrops; i++)
        {
            float radius = 0.33f;

            Vector3 point = GenerateRandomPointOnEdge(transform.position, radius);
            point.z = 1;

            var bloodDrop = bloodObjectPooler.GetPooledObject();
            bloodDrop.transform.position = point;
            bloodDrop.transform.parent = GameObject.FindGameObjectWithTag("Bloodbag").transform;
            bloodDrop.SetActive(true);

        }
        //disapear into void (refactor would be send back to the object pooling )
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public Vector2 GenerateRandomPointOnEdge(Vector2 originalPoint, float radius)
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        // Calculate the coordinates on the edge of the specified radius
        float randomX = originalPoint.x + Mathf.Cos(randomAngle) * radius;
        float randomY = originalPoint.y + Mathf.Sin(randomAngle) * radius;

        // Create a new Vector2 using the random coordinates
        Vector2 randomPoint = new Vector2(randomX, randomY);

        return randomPoint;
    }
    public void SetBloodDropAmt(int amt) {
        amtOfBloodDrops = amt;
    }
}
