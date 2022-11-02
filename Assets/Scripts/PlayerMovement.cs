using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _movementInput = Vector3.zero;
    private CharacterController _charCtrl;
    private bool _lockMovement = true;

    void Awake()
    {
        _charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!_lockMovement) _movementInput = currentInput;
        else if (currentInput == Vector3.zero) _lockMovement = false;
    }

    private void FixedUpdate()
    {
        _charCtrl.Move(_movementInput * _moveSpeed * Time.deltaTime);
    }
}
