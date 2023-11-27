using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    private Health enemyHealth;
    [SerializeField] private GameObject bloodDropPrefab;
    private int amtOfBloodDrops = 4;


    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath.AddListener(OnEnemyDeath);
        enemyHealth.OnHurt.AddListener(OnEnemyHurt);
    }
    public void OnEnemyHurt()
    {
      

    }

    public void OnEnemyDeath()
    {
     
        //enemmy death sound 
        //visual feedbacl
        //spawn x amount of droplets around me
        for (int i = 0; i < amtOfBloodDrops; i++)
        {
            float radius = 0.33f;

            Vector2 point = GenerateRandomPointOnEdge(transform.position, radius);
          
            var bloodDrop = Instantiate(bloodDropPrefab, point, Quaternion.identity);
        }
        //disapear into void (refactor would be send back to the object pooling )
        Destroy(this.gameObject);
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

}
