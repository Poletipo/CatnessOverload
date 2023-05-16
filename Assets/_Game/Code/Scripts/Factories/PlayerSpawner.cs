using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Factory {


    private static PlayerSpawner _instance;

    public static PlayerSpawner Instance {
        get {
            if (_instance == null) {
                GameManager.Instance.Create();
            }
            return _instance;
        }
    }

    public override void Initialize()
    {
        _instance = this;
    }

    [SerializeField] Player _player;

    public override void InstantiateObject()
    {
        Player player = Instantiate(_player);

        ObjectManager.AddObject(player.gameObject, _player.GetType());

    }

}
