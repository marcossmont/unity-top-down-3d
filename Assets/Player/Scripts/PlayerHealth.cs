using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead { get => _isDead; }
    
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    private PlayerShooting _playerShooting;
    private PlayerMovement _playerMovement;
    private PlayerRotation _playerRotation;
    
    private bool _isDead;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRotation = GetComponent<PlayerRotation>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();
        
        currentHealth = startingHealth;
    }


    private void Update()
    {

    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        Debug.Log("Play has been attacked.");

        if (currentHealth <= 0 && !IsDead)
        {
            Death();
        }
    }


    private void Death()
    {
        _isDead = true;

        _playerShooting.DisableEffects();

        _playerMovement.enabled = false;
        _playerRotation.enabled = false;
        _playerShooting.enabled = false;

        RestartLevel();
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
