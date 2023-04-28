using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardState
{
    public abstract void EnterState(GuardFSM guard);
    public abstract void Update(GuardFSM guard);
    public abstract void OnCollisionEnter(GuardFSM guard, Collision collision);
}
