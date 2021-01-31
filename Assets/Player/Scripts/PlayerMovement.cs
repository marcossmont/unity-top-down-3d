using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //public CharacterController controller;
    public NavMeshAgent _navMeshAgent;
    public bool isWalking;

    private InputManager _input;
    private Camera _camera;

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
            var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
            var cameraRelativeTargetVector = cameraRotationPosition * targetVector;
            _navMeshAgent.Move(cameraRelativeTargetVector * _navMeshAgent.speed * Time.deltaTime);

            isWalking = true;
            return;  
        }

        isWalking = false;
    }
}
