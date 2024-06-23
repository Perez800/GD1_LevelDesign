using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DecreaseHP(player.meleeDamage);
        }
    }
}
