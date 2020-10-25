using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    enum EnemyStates { IDLE, PATROL, CHASE, HIT, ALERT, ATTACK};
    [SerializeField]
    EnemyStates currentState;
    [SerializeField]
    Transform enemyPointer;
    [SerializeField]
    private Vector3[] patrolPositions;
    private int countPatrol = 0;
    [SerializeField] LayerMask layerMask;
    [SerializeField]
    private float alertDistance = 10;
    [SerializeField]
    private float maxDistance = 10;
    [SerializeField]
    private float minChaseDistance = 3;
    [SerializeField]
    private float maxChaseDistance = 10;
    [SerializeField]
    private float maxShootDistance = 6;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float startTimeAlert = 5.0f;
    private float timeAlert;
    [SerializeField]
    private float startTimeShoot = 1.0f;
    private float timeShoot;
    [SerializeField]
    private float startTimeHit = 1.0f;
    private float timeHit;
    

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timeAlert = startTimeAlert;
        timeShoot = 0;
        timeHit = startTimeHit;
    }


    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.IDLE:
                UpdateIDLE();
                break;
            case EnemyStates.PATROL:
                UpdatePATROL();
                break;
            case EnemyStates.ALERT:
                UpdateALERT();
                break;
            case EnemyStates.CHASE:
                UpdateCHASE();
                break;
            case EnemyStates.ATTACK:
                UpdateATTACK();
                break;
            case EnemyStates.HIT:
                UpdateHIT();
                break;
        }
    }

    private void SetState(EnemyStates newState)
    {
        switch (currentState)
        {
            case EnemyStates.PATROL:
                EndPATROL();
                break;
            case EnemyStates.CHASE:
                EndCHASE();
                break;
        }
        currentState = newState;
        switch (currentState)
        {
            case EnemyStates.IDLE:
                StartIDLE();
                break;
            case EnemyStates.ALERT:
                StartALERT();
                break;
            case EnemyStates.ATTACK:
                StartATTACK();
                break;
        }
    }

    void StartIDLE()
    {
        navMeshAgent.destination = transform.position;
    }

    void StartALERT()
    {
        timeAlert = startTimeAlert;
    }


    void StartATTACK()
    {
        timeShoot = 0;
    }


    void UpdateIDLE()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        if ((transform.position - player.transform.position).magnitude < alertDistance)
        {
            SetState(EnemyStates.ALERT);
        }
    }

    void UpdatePATROL()
    {
        navMeshAgent.destination = patrolPositions[countPatrol];
        if ((transform.position - patrolPositions[countPatrol]).magnitude < 0.1f)

            if (countPatrol == patrolPositions.Length-1)
                countPatrol = 0;
            else
                countPatrol++;

        CheckAlert();
    }

    void UpdateALERT()
    {
        if (timeAlert >= 0) {
            transform.Rotate(new Vector3(0, 30f, 0) * Time.deltaTime);
            if (Physics.Raycast(enemyPointer.position, transform.forward, out RaycastHit hitInfo, maxDistance, layerMask))
            {
                if (hitInfo.transform.gameObject.tag == "Player")
                {  
                    SetState(EnemyStates.CHASE);
                }
            }
            timeAlert -= Time.deltaTime;
        }
        else
        {
            SetState(EnemyStates.PATROL);
        }
    }

    void UpdateCHASE()
    {
        if ((transform.position - player.transform.position).magnitude > maxChaseDistance)
            SetState(EnemyStates.PATROL);
        else if ((transform.position - player.transform.position).magnitude > minChaseDistance)
            navMeshAgent.destination = player.transform.position;
        else
            SetState(EnemyStates.ATTACK);
    }

    void UpdateATTACK()
    {
        transform.LookAt(player.transform);
        if (timeShoot <= 0)
        {
            Instantiate(projectile, enemyPointer.position, enemyPointer.rotation);
            timeShoot = startTimeShoot;
        }
        else timeShoot -= Time.deltaTime;

        if ((transform.position - player.transform.position).magnitude > maxShootDistance)
            SetState(EnemyStates.CHASE);
    }

    void UpdateHIT()
    {
        if (timeHit > 0)
            timeHit -= Time.deltaTime;
        else
        {
            timeHit = startTimeHit;
            SetState(EnemyStates.ALERT);
        }
    }

        void EndPATROL()
    {
        navMeshAgent.destination = transform.position;
    }

    void EndCHASE()
    {
        navMeshAgent.destination = transform.position;
    }

    public void setHIT()
    {
        SetState(EnemyStates.HIT);
    }


}
