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

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.TryGetComponent(out Obstacle obstacle))
    //        _gameManager.OnObstacleHit(obstacle);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
            _gameManager.OnObstacleHit(obstacle);
    }
}
