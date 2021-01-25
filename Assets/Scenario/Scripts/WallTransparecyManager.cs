using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparecyManager : MonoBehaviour
{
    public Transform targetObject;
    private Camera _camera;
    private ObjectTransparencyManager oldHitObjectTransparencyManager;

    private void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>() ?? throw new Exception("Can not find Camera component.");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fromPosition = _camera.transform.position;
        Vector3 toPosition = targetObject.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                var hitObjectTransparencyManager = hit.transform.GetComponent<ObjectTransparencyManager>();

                if (oldHitObjectTransparencyManager != null && oldHitObjectTransparencyManager.name != hitObjectTransparencyManager.name)
                {
                    oldHitObjectTransparencyManager.SetTransparent(false);
                }                

                hitObjectTransparencyManager.SetTransparent(true);
                oldHitObjectTransparencyManager = hitObjectTransparencyManager;
                return;
            }
        }

        if (oldHitObjectTransparencyManager != null)
        {
            oldHitObjectTransparencyManager.SetTransparent(false);
            oldHitObjectTransparencyManager = null;
        }
    }
}
