using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }
    public bool IsFiring { get; private set; }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        InputVector = new Vector2(horizontal, vertical);

        MousePosition = Input.mousePosition;

        IsFiring = Input.GetButton("Fire1");
    }
}
