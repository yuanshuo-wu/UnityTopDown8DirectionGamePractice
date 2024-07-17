using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealGemSpawn : MonoBehaviour
{
    public Collider2D gridArea;
    public HealGem HealGemPrefab;
    private Player player;

    Vector3 spawnPoint;

    public float spawnRate = 15f;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnHealGem), spawnRate, spawnRate);
    }
    private void Update()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        spawnPoint = new Vector2(x, y);

    }
    

    public void SpawnHealGem()
    {
        HealGem healGem = Instantiate(HealGemPrefab, spawnPoint, Quaternion.identity);
    }
}
