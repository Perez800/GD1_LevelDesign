using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (player.isDetectable == true)
        {
            if (other.CompareTag("Player"))
            {
                enemy.aggroed = true;
            }
        }
    }
}
