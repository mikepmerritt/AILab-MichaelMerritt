using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatronState
{
    public abstract void EnterState(PatronFSM patron);
    public abstract void Update(PatronFSM patron);
}
