using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHP = 3;

    private int currentHP;
    void Start()
    {
        currentHP = MaxHP;
    }

    public void DecreaseHP(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
