using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startHealth = 5;
    [SerializeField] private float _freezeDuration = .3f;
    [SerializeField] private CinemachineVirtualCamera _zoomedCamera;
    [SerializeField] private PostProcessVolume _postProcessVolume;

    private int _health;
    private int _level;
    private bool _generateNextLevel = true;

    private bool _isFrozen = false;
    private float _freezeTimer;
    private Obstacle _hitObstacle;
    private UIManager _uiManager;
    private LevelGenerator _levelGenerator;
    private ColorGrading _colorGrading;
    private AudioManager _audioManager;

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
        _levelGenerator = GetComponent<LevelGenerator>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        Health = _startHealth;
        Level = 0;
        _audioManager.PlayEasy();
        _postProcessVolume.profile.TryGetSettings(out _colorGrading);
    }

    private void Update()
    {
        if (_generateNextLevel)
        {
            _levelGenerator.Generate(Level);
            _generateNextLevel = false;
            SetMusic();
            SetNewGlobalColor();
            BeginFreeze();
        }

        if (_isFrozen)
        {
            _freezeTimer -= Time.unscaledDeltaTime;
            if (_freezeTimer <= 0)
                EndFreeze();
        }
    }

    private void EndFreeze()
    {
        if (_hitObstacle != null)
        {
            _hitObstacle.OnHitEndFreeze();
            _hitObstacle = null;
            Health--;
        }
        _zoomedCamera.Priority = 0;
        _isFrozen = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public void OnObstacleHit(Obstacle obstacle)
    {
        if (!_isFrozen)
        {
            _hitObstacle = obstacle;
            _hitObstacle.OnHitStartFreeze();
            _audioManager.PlaySound("Hit");
            BeginFreeze();
        }
    }

    private void BeginFreeze()
    {
        _zoomedCamera.Priority = 2;
        _isFrozen = true;
        _freezeTimer = _freezeDuration;
        Time.timeScale = 0;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public void OnPickup(GameObject pickup)
    {
        Destroy(pickup);
        Health++;
        _audioManager.PlaySound("Pickup");
    }

    public void OnLevelComplete()
    {
        Level++;
        Debug.Log($"Survived {Level} levels!");
        _generateNextLevel = true;
        _audioManager.PlaySound("NextLevel");
    }

    private void EndGame()
    {
        _audioManager.PlaySound("Die");
        _audioManager.PlayMenu();
        GlobalPlayerData.LastScore = Level;
        SceneManager.LoadScene("Menu");
    }

    private void SetNewGlobalColor()
    {
        // use a prime number to cycle over the color space,
        // giving a deterministic unique color to every level
        _colorGrading.hueShift.value = 83 * Level;
    }

    private void SetMusic()
    {
        if (Level < 5) _audioManager.PlayEasy();
        else if (Level < 10) _audioManager.PlayMedium();
        else _audioManager.PlayHard();
    }
}
