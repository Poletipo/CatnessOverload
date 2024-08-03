using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : UIScreen
{

    public TextMeshProUGUI FinalTimerValue;
    public GameObject NewRecordTxt;
    public TextMeshProUGUI PreviousTimerTxt;
    public TextMeshProUGUI PreviousTimerValue;

    [SerializeField] GameObject VisualLayer;

    [SerializeField] GameObject _firstWidget;

    public override void Setup()
    {
        GameManager.Instance.UIManager.GetEventSystem().SetSelectedGameObject(_firstWidget);
        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed += NavigationPerformed;
    }

    private void NavigationPerformed(InputAction.CallbackContext context)
    {
        if(!VisualLayer.activeSelf){
            return;
        }

        if(GameManager.Instance.UIManager.GetEventSystem().currentSelectedGameObject != null ){
            return;
        }

        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }

    void OnDestroy(){
        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed -= NavigationPerformed;
    }

    public void QuitToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public async void ShowGameOver()
    {
        float end = Time.time + 2f;

        while (Time.time < end) {
            await Task.Yield();
        }

        VisualLayer.SetActive(true);
        Setup();

        //MoveJoystick.SetActive(false);
        //AimJoystick.SetActive(false);

        float timer = GameManager.Instance.GameplayRules.GetGameplayTimer();

        FinalTimerValue.text = TimeToString(timer);

        float bestTime = SaveTime.LoadTimeData();

        PreviousTimerValue.text = TimeToString(bestTime);

        if (bestTime < timer) {
            // New record!
            NewRecordTxt.SetActive(true);
            SaveTime.SaveTimeData(timer);
            if (bestTime == -1) {
                // no previous record
                PreviousTimerTxt.text = "";
                PreviousTimerValue.text = "";
            }
        }
        else {
            //show normal time
            NewRecordTxt.SetActive(false);
            PreviousTimerTxt.text = "Best Time:";
        }
    }

    private string TimeToString(float time)
    {
        int seconds = Mathf.FloorToInt(time) % 60;
        int minutes = Mathf.FloorToInt(time) / 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }



}
