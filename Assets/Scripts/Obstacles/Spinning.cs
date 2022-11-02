using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 10;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float angle = _rotationSpeed * Time.deltaTime;
        _rigidbody.MoveRotation(Quaternion.AngleAxis(angle, transform.up) * _rigidbody.rotation);
    }
}
