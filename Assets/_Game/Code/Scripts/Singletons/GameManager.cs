using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {

                _instance = FindFirstObjectByType<GameManager>();

                if (_instance == null) {
                    GameObject gameManagerGameObject = Resources.Load<GameObject>("GameManager");
                    GameObject managerObject = Instantiate(gameManagerGameObject);
                    _instance = managerObject.GetComponent<GameManager>();
                }

                _instance.Initialize();
                DontDestroyOnLoad(_instance);

            }
            return _instance;
        }
    }

    public PoolManager PoolManager { get; private set; }
    public GameObject CameraObject { get; private set; }
    public AudioSource AudioManager { get; private set; }
    public UIManager UIManager { get; private set; }

    public GameplayRules GameplayRules;

    Factory[] factories;

    public bool FirstTimeSession = true;

    private void Start()
    {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
    }

    public void Create()
    {
        Debug.Log("TODO: Try to create GameManager");
    }


    private void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        OnSceneLoaded();
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        PoolManager.Reset();
        ObjectManager.ResetList();
        GameplayRules = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        PoolManager = transform.Find("PoolManager").GetComponent<PoolManager>();
        AudioManager = transform.Find("AudioManager").GetComponent<AudioSource>();
        UIManager = transform.Find("- UI -").GetComponent<UIManager>();

        factories = GetComponentsInChildren<Factory>();
        foreach (Factory item in factories) {
            item.Initialize();
        }

        CameraObject = Camera.main.gameObject;

        GameplayRules = FindObjectOfType<GameplayRules>();

    }

}
