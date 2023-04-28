using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronWanderState : PatronState
{
    private bool OneWanderCompleted;
    private float TimeTillNextWander;
    private Vector3 WanderPosition;
    private float MinX = -13.5f, MaxX = 13.5f, MinZ = -13.5f, MaxZ = -3.25f;

    public override void EnterState(PatronFSM patron)
    {
        OneWanderCompleted = false;
        TimeTillNextWander = 0f;
    }

    public override void Update(PatronFSM patron)
    {
        // check for armed player nearby and run if so
        PlayerMovement Player = GameObject.FindObjectOfType<PlayerMovement>();
        if(Vector3.Distance(patron.transform.position, Player.transform.position) <= patron.CommunicationRange && Player.Armed)
        {
            patron.SetNewState(patron.FleeState);
        }

        // check if nearby patrons are scared and run if so
        PatronFSM[] AllPatrons = GameObject.FindObjectsOfType<PatronFSM>();
        foreach(PatronFSM p in AllPatrons)
        {
            if(Vector3.Distance(patron.transform.position, p.transform.position) <= patron.CommunicationRange && p.Afraid)
            {
                patron.SetNewState(patron.FleeState);
            }
        }

        // decrease the time till next wander
        TimeTillNextWander -= Time.deltaTime;

        // check the time till next wander
        if(TimeTillNextWander <= 0)
        {
            // set another wander in 2-5 sec
            TimeTillNextWander = Random.Range(2f, 5f);
            // move to random position in lobby
            WanderPosition = new Vector3(Random.Range(MinX, MaxX), 1, Random.Range(MinZ, MaxZ));
            patron.NavMeshAgent.SetDestination(WanderPosition);

            // check to see if there is an opportunity to go up front (only if already wandered once)
            if(OneWanderCompleted && patron.PatronQueue.HasRoomForPatron())
            {
                patron.SetNewState(patron.QueueState);
            }
            
            OneWanderCompleted = true;
        }
    }
}
