using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        float angle = _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(angle, transform.up) * transform.rotation;
    }
}
