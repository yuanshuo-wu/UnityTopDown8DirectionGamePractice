using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //public Player player;

    public int damageAmount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<Player>().currentHealth -= damageAmount;
            Debug.Log("current PlayerHealth: " + FindAnyObjectByType<Player>().currentHealth);
            Destroy(gameObject);
        }
    }
}
