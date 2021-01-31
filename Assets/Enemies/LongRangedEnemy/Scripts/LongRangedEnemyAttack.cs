using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangedEnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 5;
    public float range = 30f;
    private int _playerLayer;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    //EnemyHealth enemyHealth;
    private bool _playerInRange;
    private float _timer;
    private LineRenderer _gunLine;
    private float _effectsDisplayTime = 0.2f;


    void Awake()
    {
        _playerLayer = LayerMask.GetMask("Player");
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _gunLine = GetComponent<LineRenderer>();
        //enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        //Ray ray = new Ray(transform.position, _player.transform.position - transform.position);
        //Debug.DrawLine(transform.position, ray.GetPoint(range));

        _timer += Time.deltaTime;

        if (_timer >= timeBetweenAttacks) //&& enemyHealth.currentHealth > 0)
        {
            var direction = _player.transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction, out var hit, maxDistance: range, layerMask: _playerLayer))
            {
                
                Attack(hit);
            }            
        }

        if (_playerHealth.currentHealth <= 0)
        {
            Debug.Log("Player is dead.");
        }

        if (_timer >= timeBetweenAttacks * _effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        _gunLine.enabled = false;
    }




    void Attack(RaycastHit hit)
    {
        _timer = 0f;

        if (!_playerHealth.IsDead)
        {
            _gunLine.enabled = true;
            _gunLine.SetPosition(0, transform.position);
            _gunLine.SetPosition(1, hit.point);

            _playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player has been shooted.");
        }
    }
}
