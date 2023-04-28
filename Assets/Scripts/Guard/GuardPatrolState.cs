using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolState : GuardState
{
    // waypoints
    private Vector3[] Waypoints = {new Vector3(11.75f, 1f, 7f), 
                                   new Vector3(11.75f, 1f, 1f), 
                                   new Vector3(-6f, 1f, 1f), 
                                   new Vector3(-11.5f, 1f, -5f), 
                                   new Vector3(-6f, 1f, -12f), 
                                   new Vector3(7.15f, 1f, -8f), 
                                   new Vector3(-12f, 1f, 5f)};

    private int WaypointIndex;
    private PlayerMovement Player;

    public override void EnterState(GuardFSM guard)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();
        Player.Detained = false;
        WaypointIndex = 0;
    }

    public override void Update(GuardFSM guard)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        // check for armed player nearby
        if(Vector3.Distance(guard.transform.position, Player.transform.position) <= guard.CommunicationRange && Player.Armed)
        {
            guard.SetNewState(guard.ChaseState);
        }

        // check if nearby patrons are scared
        PatronFSM[] AllPatrons = GameObject.FindObjectsOfType<PatronFSM>();
        foreach(PatronFSM p in AllPatrons)
        {
            if(Vector3.Distance(guard.transform.position, p.transform.position) <= guard.CommunicationRange && p.Afraid)
            {
                guard.SetNewState(guard.ChaseState);
            }
        }

        if(Vector3.Distance(guard.transform.position, Waypoints[WaypointIndex]) > 0.1f)
        {
            guard.NavMeshAgent.SetDestination(Waypoints[WaypointIndex]);
        }
        else
        {
            WaypointIndex = (WaypointIndex + 1) % Waypoints.Length;
        }
    }

    public override void OnCollisionEnter(GuardFSM guard, Collision collision)
    {
        // do nothing
    }
}
