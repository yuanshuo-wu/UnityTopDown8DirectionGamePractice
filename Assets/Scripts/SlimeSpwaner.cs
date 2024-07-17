using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpwaner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoint;

    public float timeBetweenSpawns;
    float nextSpawnTime;

    
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            Instantiate(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
            nextSpawnTime = Time.time + timeBetweenSpawns;
        }
    }
}
