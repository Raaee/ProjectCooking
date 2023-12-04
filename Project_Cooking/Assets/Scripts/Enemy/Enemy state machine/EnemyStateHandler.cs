using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField] private EnemyState initialState;
    [Header("DEBUG")]
    [SerializeField] private EnemyState currentState;

    private void Start()
    {
        currentState = initialState;
        currentState.OnStateEnter();
    }


    private void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }
}
