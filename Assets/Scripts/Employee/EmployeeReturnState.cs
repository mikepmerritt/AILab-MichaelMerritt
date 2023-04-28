using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeReturnState : EmployeeState
{
    private PlayerMovement Player;

    public override void EnterState(EmployeeFSM employee)
    {
        employee.Money.SetActive(true);
    }

    public override void Update(EmployeeFSM employee)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        // check for armed player nearby
        if(Vector3.Distance(employee.transform.position, Player.transform.position) <= employee.CommunicationRange && Player.Armed)
        {
            employee.SetNewState(employee.PanicState);
        }
        else if(Vector3.Distance(employee.transform.position, employee.CashRegisterPosition) > 0.2f)
        {
            employee.NavMeshAgent.SetDestination(employee.CashRegisterPosition);
        }
        else
        {
            if(employee.PatronQueue.PatronsInLine.Count > 0)
            {
                employee.PatronQueue.PatronsInLine[0].SetNewState(employee.PatronQueue.PatronsInLine[0].WanderState); // make customer leave line
                employee.PatronQueue.RemovePatron(employee.PatronQueue.PatronsInLine[0]); // remove customer from line
            }
            employee.SetNewState(employee.IdleState); // return to idling
        }
    }
}