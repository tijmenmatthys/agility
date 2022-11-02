using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float _startHeight = 10;
    [SerializeField] private float _maxFallDelay = 10;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private float _timer = 0;
    private bool _isFallStarted = false;
    private float _fallDelay;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _rigidbody.useGravity = false;
        _meshRenderer.enabled = false;
        _fallDelay = Random.Range(0, _maxFallDelay);

        Vector3 position = _rigidbody.position;
        position.y = _startHeight;
        _rigidbody.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFallStarted)
        {
            _timer += Time.deltaTime;
            if (_timer > _fallDelay)
                StartFall();
        }
    }

    private void StartFall()
    {
        _isFallStarted = true;
        _rigidbody.useGravity = true;
        _meshRenderer.enabled = true;
    }
}
