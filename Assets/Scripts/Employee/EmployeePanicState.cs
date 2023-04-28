using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePanicState : EmployeeState
{
    private PlayerMovement Player;

    public override void EnterState(EmployeeFSM employee)
    {
        employee.Money.SetActive(true);
    }

    public override void Update(EmployeeFSM employee)
    {
        Player = GameObject.FindObjectOfType<PlayerMovement>();

        if(Vector3.Distance(employee.transform.position, Player.transform.position) <= employee.CommunicationRange && Player.Armed)
        {
            if(Vector3.Distance(employee.transform.position, employee.CashRegisterPosition) > 0.2f)
            {
                employee.NavMeshAgent.SetDestination(employee.CashRegisterPosition);
            }
            else
            {
                // if the player is at the cash register, give them the money
                if(employee.OrderingZone.GetComponent<OrderingZone>().OccupiedPlayer && !Player.HasMoney)
                {
                    Player.GiveMoney();
                }
            }
        }
        else
        {
            employee.SetNewState(employee.IdleState); // return to idling
        }
        
    }
}
