using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronQueueState : PatronState
{
    private Vector3 PositionInLine;

    public override void EnterState(PatronFSM patron)
    {
        if(!patron.PatronQueue.TryAddPatron(patron))
        {
            patron.SetNewState(patron.WanderState); // ran out of room, so send back to wandering
        }
    }

    public override void Update(PatronFSM patron)
    {
        // move to spot in line
        if(PositionInLine != null)
        {
            patron.NavMeshAgent.SetDestination(PositionInLine);
        }
        // if it hasn't been set yet, just wait

        // check for armed player nearby and run if so
        PlayerMovement Player = GameObject.FindObjectOfType<PlayerMovement>();
        if(Vector3.Distance(patron.transform.position, Player.transform.position) <= patron.CommunicationRange && Player.Armed)
        {
            patron.PatronQueue.RemovePatron(patron);
            patron.SetNewState(patron.FleeState);
        }

        // check if nearby patrons are scared and run if so
        PatronFSM[] AllPatrons = GameObject.FindObjectsOfType<PatronFSM>();
        foreach(PatronFSM p in AllPatrons)
        {
            if(Vector3.Distance(patron.transform.position, p.transform.position) <= patron.CommunicationRange && p.Afraid)
            {
                patron.PatronQueue.RemovePatron(patron);
                patron.SetNewState(patron.FleeState);
            }
        }
    }

    public void SetPositionInLine(Vector3 pos)
    {
        PositionInLine = pos;
    }
}
