    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealGem : MonoBehaviour
{

    Player playerHealth;

    public int healthBouns = 4;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.currentHealth < playerHealth.totalHealth) {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                playerHealth.currentHealth += healthBouns;
            }
        }
    }


}
