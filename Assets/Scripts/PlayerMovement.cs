using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _movementInput;
    private CharacterController _charCtrl;

    // Start is called before the first frame update
    void Start()
    {
        _charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        _charCtrl.Move(_movementInput * _moveSpeed * Time.deltaTime);
    }
}
