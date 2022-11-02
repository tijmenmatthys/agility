using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private int _obstacleLayer;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == _obstacleLayer)
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
            _gameManager.OnObstacleHit(obstacle);
    }
}
