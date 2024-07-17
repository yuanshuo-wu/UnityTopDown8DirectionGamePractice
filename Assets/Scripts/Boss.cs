using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Player player;
    private Rigidbody2D monsterRb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SimpleFlash flashEffect;

    public float BossSpeed;
    public float BossHealth;
    public int damageAmount;
    private Vector2 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }


    private void Start()
    {
        player = FindObjectOfType<Player>();
        monsterRb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        SpriteFlip();

        monsterRb.position = Vector2.MoveTowards(monsterRb.position, player.transform.position, BossSpeed * Time.deltaTime);
    }

    void SpriteFlip()
    {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void TakeDamage(float damageToTake)
    {

        flashEffect.Flash();
        BossHealth -= damageToTake;
        Debug.Log("current bossHealth: " + BossHealth);

        if (BossHealth <= 0)
        {
            flashEffect.Flash();
            Destroy(gameObject);
            Time.timeScale = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<Player>().currentHealth -= damageAmount;
            Debug.Log("current PlayerHealth: " + FindAnyObjectByType<Player>().currentHealth);
        }
    }

}
