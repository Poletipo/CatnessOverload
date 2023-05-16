using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoBehaviour {

    private static Dictionary<GameObject, List<GameObject>> PooledList = new Dictionary<GameObject, List<GameObject>>();

    //public static T Instantiate<T>(T original) where T : Object
    public static T GetPoolObject<T>(T spawneObject) where T : UnityEngine.Object
    {
        GameObject obj = spawneObject as GameObject;

        if (!PooledList.ContainsKey(obj)) {
            PooledList.Add(obj, new List<GameObject>());
            GameObject parent = new GameObject();

            parent.name = obj.name + "_Pooler";

            PooledList[obj].Add(parent);
        }

        List<GameObject> pooledObjects = PooledList[obj];
        for (int i = 0; i < pooledObjects.Count; i++) {

            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i] as T;
            }
        }

        GameObject tmp = Instantiate(obj, pooledObjects[0].transform);
        pooledObjects.Add(tmp);

        return tmp as T;
    }


}
