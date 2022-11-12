using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private LayerMask _spawnLayer;
    [SerializeField] private int _failedSpawnRetries = 10;
    [SerializeField] private int _maxBlockWidth = 5;

    [Space(10)]
    [SerializeField] private int _blocks;
    [SerializeField] private int _spinningBlocks;
    [SerializeField] private int _fallingBlocks;
    [SerializeField] private int _balls;
    [SerializeField] private int _pickups;

    [Space(10)]
    [SerializeField] private float _extraBlocks;
    [SerializeField] private float _extraSpinningBlocks;
    [SerializeField] private float _extraFallingBlocks;
    [SerializeField] private float _extraBalls;
    [SerializeField] private float _extraPickups;

    [Space(10)]
    [SerializeField] private Transform _container;

    [Space(10)]
    [SerializeField] private GameObject _environmentPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private GameObject _spawnProtectorPrefab;
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private GameObject _spinningBlockPrefab;
    [SerializeField] private GameObject _fallingBlockPrefab;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _pickupPrefab;

    private int _difficulty;
    private int _spawnSuccessCount;
    private int _spawnFailCount;
    private int _spawnRetryCount;
    private float _margin = 1.5f;
    private float _marginObstacle = .5f;
    private GameObject _startSpawnProtector;
    private GameObject _finishSpawnProtector;
    private GameObject _player;

    private int Blocks => _blocks + (int)(_extraBlocks * _difficulty);
    private int SpinningBlocks => _spinningBlocks + (int)(_extraSpinningBlocks * _difficulty);
    private int FallingBlocks => _fallingBlocks + (int)(_extraFallingBlocks * _difficulty);
    private int Balls => _balls + (int)(_extraBalls * _difficulty);
    private int Pickups => _pickups + (int)(_extraPickups * _difficulty);

    public void Generate(int difficulty)
    {

        _difficulty = difficulty;
        _spawnSuccessCount = 0;
        _spawnFailCount = 0;

        ClearLevel();
        SpawnEnvironment();
        SpawnPlayer();
        SpawnFinish();
        SpawnBlocks();
        SpawnBalls();
        SpawnPickups();
        SpawnSpinningBlocks();
        SpawnFallingBlocks();
        Cleanup();

        Debug.Log($"Level generated: {_spawnSuccessCount} obstacles spawned, {_spawnRetryCount} retries needed, {_spawnFailCount} obstacles didn't fit.");
    }

    private void SpawnPickups()
    {
        for (int i = 0; i < Pickups; i++)
            Spawn(_pickupPrefab, true, true);
    }

    private void Cleanup()
    {
        Destroy(_startSpawnProtector);
        Destroy(_finishSpawnProtector);
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < Balls; i++)
            Spawn(_ballPrefab, true, true);
    }

    private void SpawnFallingBlocks()
    {
        for (int i = 0; i < FallingBlocks; i++)
            Spawn(_fallingBlockPrefab, false, true);
    }

    private void SpawnSpinningBlocks()
    {
        for (int i = 0; i < SpinningBlocks; i++)
            Spawn(_spinningBlockPrefab, true);
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < Blocks; i++)
            Spawn(_blockPrefab);
    }

    private void Spawn(GameObject prefab, bool checkCircle = false, bool dontScale = false)
    {
        SimulatePhysics();

        for (int i = 0; i < _failedSpawnRetries; i++)
        {
            float xPosition = Random.Range(-_size / 2 + _marginObstacle, _size / 2 - _marginObstacle);
            float zPosition = Random.Range(-_size / 2 + _marginObstacle, _size / 2 - _marginObstacle);
            Quaternion rotation = Random.Range(0f, 1f) < .5 ? Quaternion.identity : Quaternion.AngleAxis(90, Vector3.up);
            float xSize = dontScale ? 1 : Random.Range(1, _maxBlockWidth);

            if (IsNotObstructed(xSize, checkCircle, xPosition, zPosition, rotation))
            {
                Vector3 spawnPosition = new Vector3(xPosition, prefab.transform.position.y, zPosition);
                GameObject obstacle = Instantiate(prefab, spawnPosition, rotation, _container);

                var scale = obstacle.transform.localScale;
                scale.x = xSize;
                obstacle.transform.localScale = scale;

                _spawnSuccessCount++;
                return;
            }
            _spawnRetryCount++;
        }
        _spawnFailCount++;
    }

    private bool IsNotObstructed(float xSize, bool checkCircle, float xPosition, float zPosition, Quaternion rotation)
    {
        bool isFree;
        if (checkCircle)
        {
            isFree = !Physics.SphereCast(new Vector3(xPosition, 20, zPosition),
                xSize / 2, Vector3.down, out RaycastHit hit, 20, _spawnLayer);
        }
        else
        {
            isFree = !Physics.BoxCast(new Vector3(xPosition, 2, zPosition),
                new Vector3(xSize / 2, .5f, .5f), Vector3.down, out RaycastHit hitInfo, rotation,
                2, _spawnLayer);
            //if (!isFree) Debug.DrawRay(hitInfo.point, hitInfo.normal*.5f, Color.red, 999);
        }

        Color debugColor = isFree ? Color.green : Color.red;
        Debug.DrawLine(new Vector3(xPosition, 2, zPosition), new Vector3(xPosition, 2, zPosition) + Vector3.down * 2, debugColor, 999);

        return isFree;
    }

    private void SpawnFinish()
    {
        float xPosition = Random.Range(-_size / 2 + _margin, _size / 2 - _margin);
        float zPosition = _size / 2 - _margin;
        Vector3 spawnPosition = new Vector3(xPosition, _finishPrefab.transform.position.y, zPosition);

        _finishSpawnProtector = Instantiate(_spawnProtectorPrefab, spawnPosition, Quaternion.identity, _container);
        Instantiate(_finishPrefab, spawnPosition, Quaternion.identity, _container);
        //todo link player UI arrow to the finish transform
    }

    private void SpawnPlayer()
    {
        float xPosition = Random.Range(-_size / 2 + _margin, _size / 2 - _margin);
        float zPosition = -_size / 2 + _margin;
        Vector3 spawnPosition = new Vector3(xPosition, _playerPrefab.transform.position.y, zPosition);

        _startSpawnProtector = Instantiate(_spawnProtectorPrefab, spawnPosition, Quaternion.identity, _container);
        _player = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity, _container);
        foreach (var camera in FindObjectsOfType<CinemachineVirtualCamera>())
            camera.Follow = _player.transform;
    }

    private void SpawnEnvironment()
    {
        GameObject environment = Instantiate(_environmentPrefab, _container);
        //environment.transform.localScale = new Vector3(_size / 10, 1, _size / 10);
    }

    private void ClearLevel()
    {
        for (int i = _container.childCount - 1; i >= 0; i--)
            DestroyImmediate(_container.GetChild(i).gameObject);
    }

    private void SimulatePhysics()
    {
        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;
    }
}
