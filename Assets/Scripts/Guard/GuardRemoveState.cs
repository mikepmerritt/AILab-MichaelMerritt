using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRemoveState : GuardState
{
    private PlayerMovement Player;

    public override void EnterState(GuardFSM guard)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();
        Player.Detained = true;
        Player.transform.eulerAngles = new Vector3(90f, 0, 0);
        Player.Score -= 100;
    }

    public override void Update(GuardFSM guard)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();
        Player.transform.position = guard.transform.position + new Vector3(0, 2f, 0);

        guard.NavMeshAgent.SetDestination(new Vector3(0f, 0f, -18f));

        if(Player.transform.position.z < -17f)
        {
            Player.transform.position = guard.transform.position;
            Player.transform.eulerAngles = new Vector3(0, 0, 0);
            guard.SetNewState(guard.PatrolState);
        }
    }

    public override void OnCollisionEnter(GuardFSM guard, Collision collision)
    {
        // do nothing
    }
}
