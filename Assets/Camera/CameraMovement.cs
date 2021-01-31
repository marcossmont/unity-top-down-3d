using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform follow;
    public Vector3 followOffset = new Vector3(10f, 10f, -10f);
    public float speed = 5f;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = follow.position + followOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
