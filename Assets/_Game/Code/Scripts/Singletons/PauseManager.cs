using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{

    public static Action OnPause;
    public static Action OnUnPause;


    static bool _gameIsPaused = false;


    public static void ChangePauseStatus()
    {
        if (_gameIsPaused) {
            
            OnUnPause?.Invoke();
            Time.timeScale = 1;
            _gameIsPaused = false;
        }
        else {
            OnPause?.Invoke();
            Time.timeScale = 0;
            _gameIsPaused = true;
        }
    }

    public static void UnPauseGame()
    {
        if (!_gameIsPaused) {
            return;
        }
        OnUnPause?.Invoke();
        Time.timeScale = 1;
        _gameIsPaused = false;
    }

    public static void PauseGame()
    {
        if (_gameIsPaused) {
            return;
        }
        OnPause?.Invoke();
        Time.timeScale = 0;
        _gameIsPaused = true;
    }

    public static bool GetPauseStatus()
    {
        return _gameIsPaused;
    }

}
