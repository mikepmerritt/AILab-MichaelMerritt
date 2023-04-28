using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronQueueState : PatronState
{
    public override void EnterState(PatronFSM patron)
    {
        
    }

    public override void Update(PatronFSM patron)
    {
        // check for armed player nearby
        PlayerMovement Player = GameObject.FindObjectOfType<PlayerMovement>();
        if(Vector3.Distance(patron.transform.position, Player.transform.position) <= patron.CommunicationRange && Player.Armed)
        {
            patron.SetNewState(patron.FleeState);
        }

        // check if nearby patrons are scared
        PatronFSM[] AllPatrons = GameObject.FindObjectsOfType<PatronFSM>();
        foreach(PatronFSM p in AllPatrons)
        {
            if(Vector3.Distance(patron.transform.position, p.transform.position) <= patron.CommunicationRange && p.Afraid)
            {
                patron.SetNewState(patron.FleeState);
            }
        }
    }
}
