using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWall : Obstacle
{
    public override void OnHitEndFreeze()
    {
        _meshRenderer.material = _startMaterial;
    }
}
