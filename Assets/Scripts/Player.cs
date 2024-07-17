using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Vector2 direction;

    public LongSword longSwordPrefab;

    public Slider healthSlider;

    public List<Sprite> topSprite;
    public List<Sprite> toprightSprite;
    public List<Sprite> rightSprite;
    public List<Sprite> downrightSprite;
    public List<Sprite> downSprite;

    public float walkSpeed;
    public float frameRate;

    public int totalHealth = 20;
    public int currentHealth;


    float idleTime;

    private void Start()
    {
        currentHealth = totalHealth;

        healthSlider.maxValue = totalHealth;
        healthSlider.value = currentHealth;
    }
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //body.velocity = direction * walkSpeed;

        SpriteFlip();
        SetSprite();

        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        //{
        //    LongSword longSword = Instantiate(longSwordPrefab, transform.position, transform.rotation);
        //    longSword.Shoot(direction);
        //    //longSword.Shoot(transform.up);
        //}


        healthSlider.value = currentHealth;

        if (currentHealth <= 0) { 
            gameObject.SetActive(false);
            Time.timeScale = 0;
        }


    }

    private void FixedUpdate()
    {
        Vector2 position = body.position;
        Vector2 translation = walkSpeed * Time.fixedDeltaTime * direction;

        body.MovePosition(body.position + translation);
        //body.AddForce(transform.up * walkSpeed); 



    }


    void SpriteFlip() {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    List<Sprite> GetSpriteDirection() {

        List<Sprite> selectedSprites = null;

        if (direction.y > 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = toprightSprite;
            }
            else
            {
                selectedSprites = topSprite;
            }
        }
        else if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = downrightSprite;
            }
            else
            {
                selectedSprites = downSprite;
            }
        }
        else {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = rightSprite;
            }
        }

        return selectedSprites;
    }

    void SetSprite() {

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

}
