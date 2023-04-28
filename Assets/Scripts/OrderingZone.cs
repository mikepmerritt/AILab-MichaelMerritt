using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingZone : MonoBehaviour
{
    public bool OccupiedPatron, OccupiedPlayer;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            OccupiedPlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            OccupiedPlayer = false;
        }
    }
}
