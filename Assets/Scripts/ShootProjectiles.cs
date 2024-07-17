using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectiles : MonoBehaviour
{

    //[SerializeField]
    //int numberOfProjectiles;

    [SerializeField]
    GameObject projectile;

    public Boss boss;
    Vector2 startPoint;

    float radius, moveSpeed;
    public float invokeTime = 0.5f;
    public float spawnTime = 2f;

    // Use this for initialization
    void Start()
    {
        radius = 5f;
        moveSpeed = 5f;

        InvokeRepeating("SpawnProjectiles", invokeTime, spawnTime);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("m"))
        //{
        //    startPoint = boss.transform.position;
        //    SpawnProjectiles();
        //}


        startPoint = boss.transform.position;
        //SpawnProjectiles(numberOfProjectiles);

    }
    //IEnumerator ShootIEnum(GameObject projectile, float delayTime = 0f)
    //{
    //    yield return new WaitForSeconds(delayTime);

    //}

    void SpawnProjectiles()
    {
        int numberOfProjectiles = 8;
        //StartCoroutine(ShootIEnum(projectile, 1f));

        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {

            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

}