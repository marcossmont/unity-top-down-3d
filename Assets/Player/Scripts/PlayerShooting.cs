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
    private LineRenderer _gunLine;
    private InputManager _input;
    private Camera _camera;

    void Awake()
    {
        _shootableMask = LayerMask.GetMask("Floor", "Enemies");
        //_shootableMask = LayerMask.GetMask("Floor", "Walls", "Enemies");
        _gunLine = GetComponent<LineRenderer>();
        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
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
        _gunLine.enabled = false;
    }


    private void Shoot()
    {
        _timer = 0f;

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);
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
                    enemyHealth.TakeDamage(damagePerShot);
                }
                _gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                _gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }        
    }
}
