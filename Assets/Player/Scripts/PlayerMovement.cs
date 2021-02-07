using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public bool IsWalking { get => _isWalking; }
    private bool _isWalking;

    private NavMeshAgent _navMeshAgent;
    private InputManager _input;
    private Camera _camera;

    public Vector3 TargetVector { get; private set; }

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager script at InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (_input.InputVector.magnitude > 0)
        {            
            var cameraRotationPosition = Quaternion.Euler(0, _camera.gameObject.transform.rotation.eulerAngles.y, 0);
            TargetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
            var cameraRelativeTargetVector = cameraRotationPosition * TargetVector;
            _navMeshAgent.Move(cameraRelativeTargetVector * _navMeshAgent.speed * Time.deltaTime);

            _isWalking = true;
            return;  
        }

        _isWalking = false;
    }
}
