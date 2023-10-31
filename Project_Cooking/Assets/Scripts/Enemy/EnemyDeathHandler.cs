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
    }

    public void OnEnemyDeath()
    {
        Debug.Log("i lived, i die. such is the life a irrelevant enemy. :( ");
        //enemmy death sound 
        //visual feedbacl
        //spawn x amount of droplets around me
        for (int i = 0; i < amtOfBloodDrops; i++)
        {
            Vector2 point= Random.insideUnitCircle * 1f;
            Debug.Log(point);
            var bloodDrop = Instantiate(bloodDropPrefab, point, Quaternion.identity);
        }
        //disapear into void (refactor would be send back to the object pooling )
        Destroy(this.gameObject);
    }
}
