using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private int _obstacleLayer;
    private int _finishLayer;
    private int _pickupLayer;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
        _finishLayer = LayerMask.NameToLayer("Finish");
        _pickupLayer = LayerMask.NameToLayer("Pickup");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _obstacleLayer)
        {
            if (other.gameObject.TryGetComponent(out Obstacle obstacle))
                _gameManager.OnObstacleHit(obstacle);
        }
        else if (other.gameObject.layer == _pickupLayer)
            _gameManager.OnPickup((other.gameObject));
        else if (other.gameObject.layer == _finishLayer)
            _gameManager.OnLevelComplete();
    }
}
