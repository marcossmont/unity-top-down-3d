using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 followOffset = new Vector3(10f, 10f, -10f);
    public float speed = 5f;
    
    private Transform _follow;

    // Start is called before the first frame update
    void Awake()
    {
        _follow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = _follow.position + followOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = _follow.position + followOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
