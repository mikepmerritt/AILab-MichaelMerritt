using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronFleeState : PatronState
{
    public override void EnterState(PatronFSM patron)
    {
        patron.Afraid = true;
        patron.NavMeshAgent.speed *= 2;
    }
    public override void Update(PatronFSM patron)
    {
        // run outside
        patron.NavMeshAgent.SetDestination(new Vector3(0, 1, -23f));

        // if offscreen, despawn
        if(patron.transform.position.z < -20f)
        {
            GameObject.Destroy(patron.gameObject);
        }
    }
}
