using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private int _hitMask;
    private InputManager _input;
    public float speed = 5f;

    private Camera _camera;

    void Awake()
    {
        _hitMask = LayerMask.GetMask("Floor", "Buildings", "Enemies");

        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, layerMask: _hitMask))
        {
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0f;

            var targetRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}
