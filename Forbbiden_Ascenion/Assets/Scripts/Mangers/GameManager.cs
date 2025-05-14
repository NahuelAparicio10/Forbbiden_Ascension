using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null )
            {
                GameObject go = new GameObject("Game Manager");

                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private bool _isPaused = false;

    public bool IsPaused => _isPaused;

    public int timesJumped;
    public int timesTouchedCloud;
    public int timesDashed;

    public event Action OnPlayAgain;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        OnPlayAgain += PlayAgain;
    }

    private void PlayAgain()
    {
        ResetStadistics();
    }

    public void InvokePlayAgain() => OnPlayAgain?.Invoke();

    void Update()
    {
        
    }
    public void AddJump()
    {
        timesJumped++;
    }

    public void AddCloudTouched()
    {
        timesTouchedCloud++;
    }

    public void AddDash()
    {
        timesDashed++;
    }

    public void ResetStadistics()
    {
        timesDashed = 0;
        timesJumped = 0;
        timesTouchedCloud = 0;
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
    }

    private void OnDestroy()
    {
        OnPlayAgain -= PlayAgain;
    }
}
