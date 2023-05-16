using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Factory {
    private static EnemySpawner _instance;

    public static EnemySpawner Instance {
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

    [SerializeField] Enemy _enemy;


    public override void InstantiateObject()
    {
        Enemy enemy = PoolManager.GetPoolObject(_enemy);

        ObjectManager.AddObject(enemy.gameObject, _enemy.GetType());
    }
}
