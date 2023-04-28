using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EmployeeState
{
    public abstract void EnterState(EmployeeFSM employee);
    public abstract void Update(EmployeeFSM employee);
}
