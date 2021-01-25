using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float gravity = -2f;

    private InputManager _input;
    private Camera _camera;

    void Awake()
    {
        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
    }

    // Update is called once per frame
    void Update()
    {
        var calculatedSpeed = speed * Time.deltaTime;
        var calculatedGravity = controller.isGrounded ? 0 : gravity;
        if (_input.InputVector.magnitude > 0)
        {
            var cameraRotationPosition = Quaternion.Euler(0, _camera.gameObject.transform.rotation.eulerAngles.y, 0);
            var targetVector = new Vector3(_input.InputVector.x, calculatedGravity, _input.InputVector.y);
            var cameraRelativeTargetVector = cameraRotationPosition * targetVector;
            controller.Move(cameraRelativeTargetVector * calculatedSpeed);
        }
    }
}
