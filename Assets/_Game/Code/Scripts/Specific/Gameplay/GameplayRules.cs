using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayRules : MonoBehaviour
{
    public Action<float> OnTimerChanged;
    public Action OnGameEnded;

    // int DeadEnemyCount = 0;
    // int SpwanedEnemyCount = 0;

    float _gameplayTimer = 0;

    bool isGameValid = true;

    Player player;


    public Player GetPlayer(){
        return player;
    }

    public float GetGameplayTimer(){
        return _gameplayTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = ObjectManager.GetObjectsOfType<Player>()[0].GetComponent<Player>();

        player.GetHealth().OnDeath += OnPlayerDeath;
    }

    void OnDestroy(){
        player.GetHealth().OnDeath -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        if(!isGameValid){
            return;
        }

        isGameValid = false;
        OnGameEnded?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }


    void UpdateTimer(){

        if(PauseManager.GetPauseStatus() || !isGameValid){
            return;
        }
        _gameplayTimer += Time.deltaTime;
        OnTimerChanged?.Invoke(_gameplayTimer);
    }


}
