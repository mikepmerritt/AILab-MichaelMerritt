using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFetchState : EmployeeState
{
    private PlayerMovement Player;

    public override void EnterState(EmployeeFSM employee)
    {
        // do nothing
    }

    public override void Update(EmployeeFSM employee)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        // check for armed player nearby
        if(Vector3.Distance(employee.transform.position, Player.transform.position) <= employee.CommunicationRange && Player.Armed)
        {
            employee.SetNewState(employee.PanicState);
        }
        // move to safe if not there
        else if(Vector3.Distance(employee.transform.position, employee.SafePosition) > 0.2f)
        {
            employee.NavMeshAgent.SetDestination(employee.SafePosition);
        }
        // else go back to line with money
        else
        {
            employee.SetNewState(employee.ReturnState);
        }
    }
}
