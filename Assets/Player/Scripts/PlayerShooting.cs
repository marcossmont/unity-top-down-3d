using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    private float _timer;
    private float _effectsDisplayTime = 0.2f;
    private int _shootableMask;
    private InputManager _input;
    private Camera _camera;
    private ParticleSystem _gunParticles;
    private Light _gunLight;

    void Awake()
    {
        _shootableMask = LayerMask.GetMask("Floor", "Walls", "Enemies");
        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLight = GetComponent<Light>();
    }


    void Update()
    {
        _timer += Time.deltaTime;

        if (_input.IsFiring && _timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (_timer >= timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        _gunLight.enabled = false;
    }


    private void Shoot()
    {
        _timer = 0f;

        _gunParticles.Stop();
        _gunParticles.Play();
        _gunLight.enabled = true;

        Ray ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, layerMask: _shootableMask))
        {
            Vector3 playerToMouse = hitInfo.point - transform.position;

            var shootRay = new Ray();
            shootRay.origin = transform.position;
            shootRay.direction = playerToMouse;

            if (Physics.Raycast(shootRay, out var shootHit, range, _shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
            }
        }        
    }
}
