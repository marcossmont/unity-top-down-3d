using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerRotation _playerRotation;
    private PlayerHealth _playerHealth;
    private Animator _animator;
    
    public float rotationTolerance = 0.1f;

    [SerializeField]
    private float rotationAngle;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRotation = GetComponent<PlayerRotation>();
        _playerHealth = GetComponent<PlayerHealth>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerRotationPosition = Quaternion.Euler(0, _playerRotation.LookAngle, 0);
        var playerRelativeTargetVector = playerRotationPosition * _playerMovement.TargetVector;

        _animator.SetBool("IsWalking", _playerMovement.IsWalking);
        _animator.SetBool("IsDead", _playerHealth.IsDead);
        _animator.SetFloat("VerticalAxisVelocity", playerRelativeTargetVector.z);
        _animator.SetFloat("HorizontelAxisVelocity", playerRelativeTargetVector.x);
        _animator.SetFloat("RotationAngle", _playerRotation.RotationAngle);
    }
}
