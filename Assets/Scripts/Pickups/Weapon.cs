using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject sword1;
    [SerializeField] GameObject sword2;

    public int weaponTier;

    //Vector3 originalPos;
    //void Start()
    //{
    //    originalPos = new Vector3(sword1.transform.position.x, sword1.transform.position.y, 0);
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown("space"))
    //    {
    //        sword1.SetActive(false);
    //        //sword1.transform.position = originalPos;
    //        sword2.SetActive(true);

    //    }
    //}


    private void Awake()
    {
        weaponTier = 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                
                sword1.SetActive(false);
                sword2.SetActive(true);
                weaponTier++;
            }
        
    }


}
