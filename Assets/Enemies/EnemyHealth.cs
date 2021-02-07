using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int scoreValue = 10;
    //private ParticleSystem _hitParticles;
    private Slider _healthSlider;
    private int _currentHealth;
    private bool _isDead;

    public bool IsDead { get => _isDead; }

    void Awake()
    {
        //_hitParticles = GetComponentInChildren<ParticleSystem>();
        _healthSlider = GetComponentInChildren<Slider>() ?? throw new System.Exception("Can not find local Slider component.");
        _currentHealth = startingHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (IsDead)
            return;

        _currentHealth -= amount;
        _healthSlider.value = _currentHealth;

        //_hitParticles.transform.position = hitPoint;
        //_hitParticles.Play();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        _isDead = true;
        Destroy(gameObject);
        ScoreManager.score += scoreValue;
    }
}
