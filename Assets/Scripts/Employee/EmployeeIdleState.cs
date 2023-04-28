using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeIdleState : EmployeeState
{
    private PlayerMovement Player;

    public override void EnterState(EmployeeFSM employee)
    {
        employee.Money.SetActive(false);
    }

    public override void Update(EmployeeFSM employee)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        // check for armed player nearby
        if(Vector3.Distance(employee.transform.position, Player.transform.position) <= employee.CommunicationRange && Player.Armed)
        {
            employee.SetNewState(employee.PanicState);
        }
        // stand still at cash register and stare at line
        else if(Vector3.Distance(employee.transform.position, employee.CashRegisterPosition) > 0.2f)
        {
            employee.NavMeshAgent.SetDestination(employee.CashRegisterPosition);
        }
        else
        {
            // employee.transform.LookAt(employee.CashRegisterPosition + Vector3.back);

            // if there is someone in the line, start getting them money
            if(employee.PatronQueue.PatronsInLine.Count > 0)
            {
                employee.SetNewState(employee.FetchState);
            }
        }
    }
}
