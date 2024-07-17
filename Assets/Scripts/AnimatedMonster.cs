using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedMonster : MonoBehaviour
{
    public Sprite[] walkSprites = new Sprite[0];
    //public Sprite[] attackeSprites = new Sprite[0];
    public Sprite[] hitSprites = new Sprite[0];
    public float animationTime = 0.25f;
    public bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    
    // Monster Movement
    public Player player;
    private Rigidbody2D monsterRb;
    [SerializeField] private SimpleFlash flashEffect;

    public int score = 3;
    public float monsterSpeed;
    public float monsterHealth;
    public int damageAmount;
    public float knockbackDuration = 0.5f;
    private float knockbackCounter;
    float idleTime;
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
        InvokeRepeating(nameof(Advance), animationTime, animationTime);

        player = FindObjectOfType<Player>();
        monsterRb = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        SpriteFlip();

    }

    private void FixedUpdate()
    {
        monsterRb.position = Vector2.MoveTowards(monsterRb.position, player.transform.position, monsterSpeed * Time.deltaTime);

        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            if (monsterSpeed > 0)
            {
                monsterSpeed = -monsterSpeed * 2;
            }

            if (knockbackCounter <= 0)
            {
                monsterSpeed = Mathf.Abs(monsterSpeed * 0.5f);
            }

        }

    }

    private void Advance()
    {
        if (!spriteRenderer.enabled)
        {
            return;
        }

        animationFrame++;

        if (animationFrame >= walkSprites.Length && loop)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < walkSprites.Length)
        {
            spriteRenderer.sprite = walkSprites[animationFrame];
        }
    }

    private void Hit()
    {
        if (!spriteRenderer.enabled)
        {
            return;
        }

        animationFrame++;

        if (animationFrame >= hitSprites.Length && loop)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < hitSprites.Length)
        {
            spriteRenderer.sprite = hitSprites[animationFrame];
        }
    }


    public void Restart()
    {
        animationFrame = -1;

        Advance();
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

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
        //print("WaitAndPrint " + Time.time);
    }


    public void TakeDamage(float damageToTake)
    {
        InvokeRepeating(nameof(Hit), animationTime, animationTime);
        //WaitOneSecond();
        //CancelInvoke();

        flashEffect.Flash();
        monsterHealth -= damageToTake;
        //Debug.Log("current enemyHealth: " + monsterHealth);

        if (monsterHealth <= 0)
        {
            GameManager.Instance.OnSkeletonKilled(this);
            flashEffect.Flash();
            Destroy(gameObject);
        }

        knockbackCounter = knockbackDuration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<Player>().currentHealth -= damageAmount;
            //Debug.Log("current PlayerHealth: " + FindAnyObjectByType<Player>().currentHealth);
        }
    }


}

