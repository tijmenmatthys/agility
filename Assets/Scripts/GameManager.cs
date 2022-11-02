using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startHealth = 5;
    [SerializeField] private float _freezeDuration = .3f;
    [SerializeField] CinemachineVirtualCamera _zoomedCamera;

    private int _health;
    private int _level;

    private bool _isFrozen = false;
    private float _freezeTimer;
    private Obstacle _hitObstacle;
    private UIManager _uiManager;

    private int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            _uiManager.UpdateHealth(value);
            if (value <= 0) EndGame();
        }
    }
    private int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            _uiManager.UpdateLevel(value);
        }
    }

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }
    void Start()
    {
        Health = _startHealth;
        Level = 1;
    }

    private void Update()
    {
        if (_isFrozen)
        {
            _freezeTimer -= Time.unscaledDeltaTime;
            if (_freezeTimer <= 0)
                EndFreeze();
        }
    }

    private void EndFreeze()
    {
        _hitObstacle.OnHitEndFreeze();
        _zoomedCamera.Priority = 0;
        _isFrozen = false;
        Time.timeScale = 1;
        Health--;
    }

    public void OnObstacleHit(Obstacle obstacle)
    {
        if (!_isFrozen)
        {
            _hitObstacle = obstacle;
            _hitObstacle.OnHitStartFreeze();
            _zoomedCamera.Priority = 2;
            _isFrozen = true;
            _freezeTimer = _freezeDuration;
            Time.timeScale = 0;
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }
}
