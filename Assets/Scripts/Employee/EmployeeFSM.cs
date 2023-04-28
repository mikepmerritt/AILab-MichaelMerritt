using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeFSM : MonoBehaviour
{
    private EmployeeState CurrentState;
    public NavMeshAgent NavMeshAgent;
    public float CommunicationRange;

    public PatronQueue PatronQueue;
    public GameObject OrderingZone, Money;
    public Vector3 CashRegisterPosition = new Vector3(-6.28f, 1f, 11.8f);
    public Vector3 SafePosition = new Vector3(11.5f, 1f, 11.8f);

    // four behavior types: idle, fetch cash from safe, return with cash, and panic (give the player money when threatened)
    public readonly EmployeeIdleState IdleState = new EmployeeIdleState();
    public readonly EmployeeFetchState FetchState = new EmployeeFetchState();
    public readonly EmployeeReturnState ReturnState = new EmployeeReturnState();
    public readonly EmployeePanicState PanicState = new EmployeePanicState();

    private void Awake()
    {
        // set agent
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // set up communication variables
        CommunicationRange = 5f;

        // get queue
        PatronQueue = GameObject.FindObjectOfType<PatronQueue>();

        // get ordering zone
        OrderingZone = GameObject.Find("ServingPosition");
    }

    private void Start()
    {
        SetNewState(IdleState);
    }

    private void Update()
    {
        CurrentState.Update(this);
    }

    public void SetNewState(EmployeeState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}
