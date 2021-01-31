using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private GameObject _player;
    private PlayerHealth _playerHealth;
    //EnemyHealth enemyHealth;
    private bool _playerInRange;
    private float _timer;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = false;
        }
    }


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenAttacks && _playerInRange) //&& enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.currentHealth <= 0)
        {
            Debug.Log("Player is dead.");
        }
    }


    void Attack()
    {
        _timer = 0f;

        if (_playerHealth.currentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player has been attacked.");
        }
    }
}
