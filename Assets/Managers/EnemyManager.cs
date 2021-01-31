using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime = 2f;
    public Transform[] spawnPoints;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        _playerHealth = player?.GetComponent<PlayerHealth>() ?? throw new System.Exception("Can not find PlayerHealth component at Player game object.");
    }

    void Start()
    {   
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (_playerHealth.IsDead) return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemies.Length);

        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
