using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                Debug.Log("Creation gamemanager");
                GameObject gameManagerGameObject = Resources.Load<GameObject>("GameManager");
                GameObject managerObject = Instantiate(gameManagerGameObject);
                _instance = managerObject.GetComponent<GameManager>();
                _instance.Initialize();

                DontDestroyOnLoad(_instance);

            }
            return _instance;
        }
    }

    public PoolManager PoolManager { get; private set; }
    public GameObject Player { get; private set; }
    public GameObject CameraObject { get; private set; }
    public AudioSource AudioManager { get; private set; }
    public GameUI GameUI { get; private set; }
    public PauseUI PauseUI { get; private set; }

    public bool FirstTimeSession = true;

    private void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        PoolManager = transform.Find("PoolManager").GetComponent<PoolManager>();
        AudioManager = transform.Find("AudioManager").GetComponent<AudioSource>();
        CameraObject = Camera.main.gameObject;
    }

}
