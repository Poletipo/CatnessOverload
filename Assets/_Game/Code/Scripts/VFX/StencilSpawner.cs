using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StencilSpawner : MonoBehaviour
{
    private static Dictionary<string, List<GameObject>> StencilList;

    private void Start()
    {
        StencilList = new Dictionary<string, List<GameObject>>();
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        StencilList.Clear();
    }

    public static void SpawnStencil(string spawnName, GameObject spawnObject, float spawnDistance, Vector3 position, Quaternion rotation)
    {
        bool validSpawn = true;

        if (StencilList.ContainsKey(spawnName)) {

            float sqrSpawnDistance = spawnDistance * spawnDistance;

            for (int i = 1; i < StencilList[spawnName].Count; i++) {
                Vector3 distanceVector = position - StencilList[spawnName][i].transform.position;

                float distance = Vector3.SqrMagnitude(distanceVector);

                if (distance < sqrSpawnDistance) {
                    validSpawn = false;
                    break;
                }
            }
        }
        else {
            StencilList.Add(spawnName, new List<GameObject>());
            StencilList[spawnName].Add(new GameObject(spawnName + "_Stencil"));
        }

        if (validSpawn) {

            GameObject tmp = Instantiate(spawnObject, position, rotation);
            tmp.transform.parent = StencilList[spawnName][0].transform;
            StencilList[spawnName].Add(tmp);
        }
    }



}
