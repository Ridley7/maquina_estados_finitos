using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    [Header("Path")]
    [SerializeField] private GameObject[] wandarPoints;

    [Header("Player")]
    [SerializeField] private Transform playerTank;

    [SerializeField] private Transform turret;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject bulletSpawnPos;

    private Vector3 nextDestination;
    private float minDistanceToAttack = 10f;
    private float shootRate = 2f;
    private float elapsedTime;

    public void ChangeState(AIState newState)
    {
        if(remainState != newState)
        {
            currentState = newState;
        }

    }

    #region Actions

    public void FindNextDestination()
    {
        int randomIndex = Random.Range(0, wandarPoints.Length);
        nextDestination = wandarPoints[randomIndex].transform.position;
    }

    public void MoveAndRotateTowardsDestination()
    {
        Quaternion targetRotation = Quaternion.LookRotation(nextDestination - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        transform.Translate(Vector3.forward * 6f * Time.deltaTime);
    }

    public void UpdateNextDestinationTowardsPlayer()
    {
        if (playerTank != null)
        {
            nextDestination = playerTank.position;
        }
    }

    public void RotateTurretTowardsTarget()
    {
        Quaternion newRotation = Quaternion.LookRotation(playerTank.position - transform.position);
        transform.rotation = Quaternion.Slerp(turret.rotation, newRotation, Time.deltaTime * 10f);
    }

    #endregion

    #region Decisions

    public bool CloseToDestination()
    {
        float distance = Vector3.Distance(transform.position, nextDestination);

        if (distance <= 5.0f)
        {
            return true;
        }

        return false;
    }

    public bool PlayerInRangeToChase()
    {
        float distanceToPlayer = Vector3.Distance(playerTank.position, transform.position);

        if (distanceToPlayer <= 15.0f)
        {
            return true;
        }
        
        return false;
    }

    public void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(prefabBullet, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
            elapsedTime = 0;
        }
    }

    public bool PlayerInRangeToAttack()
    {
        float distanceToPlayer = Vector3.Distance(playerTank.position, transform.position);

        if (distanceToPlayer <= minDistanceToAttack)
        {
            return true;
        }

        return false;
    }

    #endregion

    private void Start()
    {
        FindNextDestination();
    }

    private void Update()
    {
        currentState.RunState(this);
        elapsedTime += Time.deltaTime;
    }

    

    
}
