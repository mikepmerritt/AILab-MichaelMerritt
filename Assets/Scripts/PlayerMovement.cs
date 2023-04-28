using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject Weapon;
    public bool Armed, Detained;

    void FixedUpdate()
    {
        if(!Detained)
        {
            Rb.velocity = Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")));
            Rb.velocity *= Speed;

            if(Input.GetKey(KeyCode.Space))
            {
                Weapon.SetActive(true);
                Armed = true;
            }
            else
            {
                Weapon.SetActive(false);
                Armed = false;
            }
        }
        else
        {
            Weapon.SetActive(false);
            Armed = false;
        }
    }
}
