using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatronFSM : MonoBehaviour
{
    private PatronState CurrentState;
    private Material ShirtMaterial;

    public NavMeshAgent NavMeshAgent;
    public PatronQueue PatronQueue;
    public bool Afraid;
    public float CommunicationRange;

    public readonly PatronWanderState WanderState = new PatronWanderState();
    public readonly PatronQueueState QueueState = new PatronQueueState();
    public readonly PatronFleeState FleeState = new PatronFleeState();

    private void Awake()
    {
        // set shirt color
        ShirtMaterial = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        ShirtMaterial.SetColor("_Color", new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), 1));

        // set agent
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // set up communication variables
        Afraid = false;
        CommunicationRange = 5f;

        // set up queues
        PatronQueue = GameObject.FindObjectOfType<PatronQueue>();
    }

    private void Start()
    {
        SetNewState(WanderState);
    }

    private void Update()
    {
        CurrentState.Update(this);
    }

    public void SetNewState(PatronState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}
