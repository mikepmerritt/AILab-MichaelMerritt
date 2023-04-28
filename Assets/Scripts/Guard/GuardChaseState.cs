using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChaseState : GuardState
{
    private PlayerMovement Player;

    public override void EnterState(GuardFSM guard)
    {
        guard.NavMeshAgent.speed = 5f;
    }

    public override void Update(GuardFSM guard)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        guard.NavMeshAgent.SetDestination(Player.transform.position);

        if(Player.transform.position.z < -15f)
        {
            guard.SetNewState(guard.PatrolState);
        }
    }

    public override void OnCollisionEnter(GuardFSM guard, Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            guard.SetNewState(guard.RemoveState);
        }
    }
}
