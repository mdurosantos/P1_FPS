using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    enum EnemyStates { HIT, ALERT, ATTACK};
    EnemyStates currentState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(EnemyStates.ALERT);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.ALERT:
                UpdateEstat1();
                break;
            case EnemyStates.HIT:
                UpdateEstat2();
                break;
            case EnemyStates.ATTACK:
                UpdateEstat3();
                break;
        }
    }

    private void SetState(EnemyStates newState)
    {
        switch (currentState)
        {
            case EnemyStates.ALERT:
                EndEstat1();
                break;
            case EnemyStates.HIT:
                EndEstat2();
                break;
            case EnemyStates.ATTACK:
                EndEstat3();
                break;
        }
        currentState = newState;
        switch (currentState)
        {
            case EnemyStates.ALERT:
                StartEstat1();
                break;
            case EnemyStates.HIT:
                StartEstat2();
                break;
            case EnemyStates.ATTACK:
                StartEstat3();
                break;
        }
    }

    void StartEstat1()
    {

    }

    void StartEstat2()
    {

    }

    void StartEstat3()
    {

    }

    void UpdateEstat1()
    {

    }

    void UpdateEstat2()
    {

    }

    void UpdateEstat3()
    {

    }

    void EndEstat1()
    {

    }

    void EndEstat2()
    {

    }

    void EndEstat3()
    {

    }
}
