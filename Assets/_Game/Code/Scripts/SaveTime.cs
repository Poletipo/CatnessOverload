using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveTime {

    [System.Serializable]
    public struct CatData{
        public string mapName;
        public float time;
    }

    static string filePathDirectory = Application.persistentDataPath + "/CatnessOverload.dat";


    public static bool SaveExist()
    {
        if (File.Exists(filePathDirectory)) {
            return true;
        }
        return false;
    }

    public static void SaveTimeData(string name, float time)
    {

        List<CatData> data = new List<CatData>();

        if(SaveExist()){
            data = LoadTimeData();
        }

        

        CatData tempData = new CatData(){mapName = name, time = time};
        if(ContainsData(name)){ // TODO : This loads a second time the file. Not good
            
            for (int i = 0; i < data.Count; i++)
            {
                if(data[i].mapName == name){
                    data[i] = tempData;
                }
            }

        }
        else{
            data.Add(tempData);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(filePathDirectory, FileMode.Create);
        bf.Serialize(file, data);
        file.Close();
    }

    public static List<CatData> LoadTimeData()
    {
        if (SaveExist()) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePathDirectory, FileMode.Open);
            List<CatData> data = (List<CatData>)bf.Deserialize(fs);
            fs.Close();
            return data;
        }
        else {
            return null;
        }
    }

    public static void DeleteData()
    {
        if (SaveExist()) {
            File.Delete(filePathDirectory);
        }
    }

    public static bool ContainsData(string name){

        if(!SaveExist()){
            return false;
        }

        List<CatData> data = LoadTimeData();


        for (int i = 0; i < data.Count; i++)
        {
            if(data[i].mapName == name){
                return true;
            }
        }

        return false;
    }

    public static CatData GetData(string name){

        List<CatData> data = LoadTimeData();

        for (int i = 0; i < data.Count; i++)
        {
            if(data[i].mapName == name){
                return data[i];
            }
        }
        
        return data[0];

    } 


}
