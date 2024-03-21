using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static Dictionary<Type, List<GameObject>> objectDictionary = new Dictionary<Type, List<GameObject>>();
    public static void AddObject(GameObject obj, Type objectType)
    {

        Debug.Log($"Added {obj.name} : {objectType}");

        if (!objectDictionary.ContainsKey(objectType)) {
            objectDictionary[objectType] = new List<GameObject>();
        }
        objectDictionary[objectType].Add(obj);
    }

    public static List<GameObject> GetObjectsOfType<T>()
    {
        Type objectType = typeof(T);


        if (!objectDictionary.ContainsKey(objectType)) {
            UnityEngine.Object[] obj = FindObjectsOfType(objectType);

            //Array.ConvertAll<T, GameObject>(FindObjectsOfType(objectType), (item) => item as GameObject);


            foreach (UnityEngine.Object item in obj) {

                MonoBehaviour gObj = item as MonoBehaviour;

                AddObject(gObj.gameObject, objectType);
            }
        }

        if (objectDictionary.ContainsKey(objectType)) {
            return objectDictionary[objectType];
        }
        return null;
    }
}