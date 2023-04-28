using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;
    public float Speed;
    public GameObject Weapon, Money;
    public bool Armed, Detained, HasMoney;
    public int Score = 0;
    public TMP_Text ScoreText;

    void FixedUpdate()
    {
        ScoreText.text = "Score: " + Score;

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
            HasMoney = false;
            Money.SetActive(false);
        }

        if(HasMoney && transform.position.z < -16f)
        {
            Score += 500;
            HasMoney = false;
            Money.SetActive(false);
        }
    }

    public void GiveMoney()
    {
        HasMoney = true;
        Money.SetActive(true);
    }
}
