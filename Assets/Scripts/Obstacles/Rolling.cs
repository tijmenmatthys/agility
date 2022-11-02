using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMovement();
    }

    private void InitMovement()
    {
        float movementAngle = Random.Range(0, 360);
        _rigidbody.velocity = Quaternion.AngleAxis(movementAngle, Vector3.up) * Vector3.right * _moveSpeed;
    }
}
