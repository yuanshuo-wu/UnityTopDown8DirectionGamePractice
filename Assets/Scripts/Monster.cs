using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    //public Transform player;
    public Player player;
    private Rigidbody2D monsterRb;

    private Vector2 direction;

    public List<Sprite> topSprite;
    public List<Sprite> rightSprite;
    public List<Sprite> downSprite;

    public int score = 1;
    public float monsterSpeed;
    public float frameRate;
    public float monsterHealth;
    public int damageAmount;
    public float knockbackDuration = 0.5f;
    private float knockbackCounter;

    float idleTime;

    [SerializeField] private SimpleFlash flashEffect;

    void Start()
    {
        player = FindObjectOfType<Player>();
        monsterRb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

        //Debug.Log("current direction: " + direction);
        //Debug.Log("current angle: " + angle);

        //spriteRenderer.transform.rotation = angle;
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.deltaTime);
        SpriteFlip();
        SetSprite();

    }

    private void FixedUpdate()
    {
        monsterRb.position = Vector2.MoveTowards(monsterRb.position, player.transform.position, monsterSpeed * Time.deltaTime);

        if (knockbackCounter > 0) { 
            knockbackCounter -= Time.deltaTime;
            if (monsterSpeed > 0) {
                monsterSpeed =- monsterSpeed*2;
            }

            if (knockbackCounter <= 0) {
                monsterSpeed = Mathf.Abs(monsterSpeed * 0.5f);
            }

        }

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

    List<Sprite> GetSpriteDirection()
    {

        List<Sprite> selectedSprites = null;

        if (direction.y > 0.5)
        {
                selectedSprites = topSprite;
        }
        else if (direction.y < -0.5)
        {
                selectedSprites = downSprite;
        }
        else
        {
                selectedSprites = rightSprite;
        }

        return selectedSprites;
    }

    void SetSprite()
    {

        List<Sprite> directionSprites = GetSpriteDirection();
        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int frame = (int)((playTime * frameRate) % directionSprites.Count);

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (inactive == false)
    //    {
    //        if (other.tag == "Hole")
    //        {
    //            player.UpdateScore();
    //            Destroy(gameObject);
    //        }
    //        if (other.tag == "Player" || other.tag == "Enemy")
    //        {
    //            player.TakeDamage();
    //            speed = 0;
    //            transform.parent = player.transform;
    //        }
    //    }
    //    inactive = true;
    //}

    public void TakeDamage(float damageToTake)
    {
        flashEffect.Flash();
        monsterHealth -= damageToTake;
        //Debug.Log("current enemyHealth: " + monsterHealth);

        if (monsterHealth <= 0) {
            GameManager.Instance.OnSlimeKilled(this);
            //flashEffect.Flash();
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
