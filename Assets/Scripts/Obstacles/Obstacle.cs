using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected Material _hitMaterial;
    protected Material _startMaterial;
    protected MeshRenderer _meshRenderer;

    protected static GameManager _gameManager;
    protected static int _playerLayer;

    private void Start()
    {
        _gameManager ??= FindObjectOfType<GameManager>();
        if (_playerLayer == 0) _playerLayer = LayerMask.NameToLayer("Player");
        _meshRenderer = GetComponent<MeshRenderer>();
        _startMaterial = _meshRenderer.material;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == _playerLayer)
    //        _gameManager.OnObstacleHit(this);
    //}

    public virtual void OnHitStartFreeze()
    {
        _meshRenderer.material = _hitMaterial;
    }

    public virtual void OnHitEndFreeze()
    {
        Destroy(gameObject);
    }
}
