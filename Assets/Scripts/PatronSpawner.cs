using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronSpawner : MonoBehaviour
{
    public GameObject PatronPrefab;
    public float Counter;

    void Update()
    {
        Counter -= Time.deltaTime;
        if(Counter < 0)
        {
            PatronFSM[] Patrons = GameObject.FindObjectsOfType<PatronFSM>();
            if(Patrons.Length < 6)
            {
                Instantiate(PatronPrefab, new Vector3(0, 1f, -20f), Quaternion.identity);
            }
            Counter = 1f;
        }
        
    }
}
