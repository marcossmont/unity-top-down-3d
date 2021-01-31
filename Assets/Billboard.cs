using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new System.Exception("Can not find Camera component.");
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }
}
