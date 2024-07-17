using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamager : MonoBehaviour
{
    public float damageAmount;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            collision.GetComponent<Monster>().TakeDamage(damageAmount);
        }

        if (collision.tag == "AnimatedEnemy")
        {
            collision.GetComponent<AnimatedMonster>().TakeDamage(damageAmount);
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damageAmount);
        }
    }
}
