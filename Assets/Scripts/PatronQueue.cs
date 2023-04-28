using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronQueue : MonoBehaviour
{
    public GameObject ServingPosition, NextPosition, BackRowPosition;
    public List<PatronFSM> PatronsInLine;

    public bool HasRoomForPatron()
    {
        return PatronsInLine.Count < 3;
    }

    public void UpdatePositionsInLine()
    {
        if(PatronsInLine.Count > 0)
        {
            PatronsInLine[0].QueueState.SetPositionInLine(ServingPosition.transform.position);
        }
        if(PatronsInLine.Count > 1)
        {
            PatronsInLine[1].QueueState.SetPositionInLine(NextPosition.transform.position);
        }
        if(PatronsInLine.Count > 2)
        {
            PatronsInLine[2].QueueState.SetPositionInLine(BackRowPosition.transform.position);
        }
    }

    public bool TryAddPatron(PatronFSM patron)
    {
        if(HasRoomForPatron())
        {
            PatronsInLine.Add(patron);
            UpdatePositionsInLine();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemovePatron(PatronFSM patron)
    {
        PatronsInLine.Remove(patron);
        UpdatePositionsInLine();
    }
}
