using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected Material _hitMaterial;
    protected Material _startMaterial;
    protected MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startMaterial = _meshRenderer.material;
    }

    public virtual void OnHitStartFreeze()
    {
        _meshRenderer.material = _hitMaterial;
    }

    public virtual void OnHitEndFreeze()
    {
        Destroy(gameObject);
    }
}
