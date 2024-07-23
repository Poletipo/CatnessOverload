using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [Header("Health")]
    [SerializeField]
    private Image _healthSlider;
    public Gradient HealthColor;
    public AnimationCurve HealthAdvancement;
    public RectTransform HealthOrigin;
    private Vector3 HealthOriginPosition;
    //public CameraShake HealthShake;

    [Header("Money")]
    [SerializeField]
    private TextMeshProUGUI _moneyValue;

    //[Header("Shop")]
    public GameObject GameOverOrigin;

    public ShopUI ShopMenu;

    [Header("Audio")]
    public Sprite AudioOn;
    public Sprite AudioOff;
    public Image AudioImageBtn;
    private bool isMuted = false;

    [Header("Timer")]
    public TextMeshProUGUI TimerValue;

    [Header("Joystick")]
    public GameObject MoveJoystick;
    public GameObject AimJoystick;

    [Header("GameOver")]
    public TextMeshProUGUI FinalTimerValue;
    public GameObject NewRecordTxt;
    public TextMeshProUGUI PreviousTimerTxt;
    public TextMeshProUGUI PreviousTimerValue;
    private float timer = 0;

    private Player _player;
    private bool playerIsDead = false;
    private Health playerHealth;

    private void Awake()
    {
        GameManager.Instance.UIManager.ClearUI();
        GameManager.Instance.UIManager.AddWidget(gameObject);
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(_player == null){
            _player = GameManager.Instance.GameplayRules.GetPlayer();
            yield return null;
        }

        if (_player != null) {

            playerHealth = _player.GetHealth();
            playerHealth.OnHpChanged += OnHpChanged;
            playerHealth.OnDeath += OnDeath;

            _player.OnMoneyChanged += OnMoneyChanged;
        }

        GameManager.Instance.GameplayRules.OnTimerChanged += UpdateTimer;


        OnMoneyChanged();
        HealthOriginPosition = HealthOrigin.position;
    }


    private void OnDestroy()
    {
        if(_player==null){
            return;
        }
        _player.OnMoneyChanged -= OnMoneyChanged;

        if(GameManager.Instance.GameplayRules==null){
            return;
        }
        GameManager.Instance.GameplayRules.OnTimerChanged -= UpdateTimer;
    }


    private async void OnDeath()
    {

        float end = Time.time + 2f;

        playerIsDead = true;

        while (Time.time < end) {
            await Task.Yield();
        }

        //MoveJoystick.SetActive(false);
        //AimJoystick.SetActive(false);
        FinalTimerValue.text = TimerValue.text;

        GameOverOrigin.SetActive(true);

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

    private void OnMoneyChanged()
    {
        _moneyValue.text = _player.MoneyAmount.ToString();
    }

    private void OnHpChanged()
    {
        float hpPercent = 1 - (float)playerHealth.Hp / playerHealth.MaxHp;

        float sliderValue = HealthAdvancement.Evaluate(hpPercent);

        _healthSlider.fillAmount = sliderValue;
        _healthSlider.color = HealthColor.Evaluate(sliderValue);
    }

    public void HideUI()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        MoveJoystick.SetActive(false);
        AimJoystick.SetActive(false);
    }

    public void ShowUI()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (!playerIsDead) {
            MoveJoystick.SetActive(true);
            AimJoystick.SetActive(true);
        }
    }

    public void UpdateTimer(float time)
    {
        timer = time;
        TimerValue.text = TimeToString(time);
    }

    private string TimeToString(float time)
    {
        int seconds = Mathf.FloorToInt(time) % 60;
        int minutes = Mathf.FloorToInt(time) / 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void ChangeMuteState()
    {
        if (!isMuted) {
            AudioListener.volume = 0;
            isMuted = true;
            AudioImageBtn.sprite = AudioOff;
        }
        else {
            AudioListener.volume = 1;
            isMuted = false;
            AudioImageBtn.sprite = AudioOn;
        }
    }

    public void OpenPauseMenu()
    {
        PauseManager.ChangePauseStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthSlider.fillAmount > 0.5f) {
            float trauma = (_healthSlider.fillAmount - 0.5f) * 2.5f;

            //HealthShake.SetTrauma(trauma);
            //HealthOrigin.position = HealthOriginPosition + HealthShake.GetPositionOffset();
        }
    }
}
