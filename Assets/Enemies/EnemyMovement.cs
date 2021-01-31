using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth _playerHealth;
    private UnityEngine.AI.NavMeshAgent nav;

    private EnemyHealth _enemyHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (!_enemyHealth.IsDead && !_playerHealth.IsDead)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
