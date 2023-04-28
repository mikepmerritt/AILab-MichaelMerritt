using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    private GuardState CurrentState;
    public NavMeshAgent NavMeshAgent;
    public float CommunicationRange;

    // three behavior types: patrol through waypoints, chase the player until they escape or it hits them, and carry the player to the exit to remove them
    public readonly GuardPatrolState PatrolState = new GuardPatrolState();
    public readonly GuardChaseState ChaseState = new GuardChaseState();
    public readonly GuardRemoveState RemoveState = new GuardRemoveState();

    private void Awake()
    {
        // set agent
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // set up communication variables
        CommunicationRange = 8f;
    }

    private void Start()
    {
        SetNewState(PatrolState);
    }

    private void Update()
    {
        CurrentState.Update(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnCollisionEnter(this, collision);
    }

    public void SetNewState(GuardState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}
