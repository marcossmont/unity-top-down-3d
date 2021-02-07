using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRotation : MonoBehaviour
{
    private int _hitMask;
    private NavMeshAgent _navMeshAgent;
    private InputManager _input;

    private Camera _camera;

    public float LookAngle { get; private set; }

    //public float LookAngle { get; private set; }

    void Awake()
    {
        _hitMask = LayerMask.GetMask("Floor", "Buildings", "Enemies");

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _input = GameObject.FindWithTag("InputManager")?.GetComponent<InputManager>() ?? throw new Exception("Can not find InputManager component.");
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, layerMask: _hitMask))
        {
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0f;

            var targetRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _navMeshAgent.angularSpeed * Time.deltaTime);

            var deltaAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, _camera.gameObject.transform.rotation.eulerAngles.y);
            LookAngle = deltaAngle < 0 ? 360 + deltaAngle : deltaAngle;
        }
    }
}
