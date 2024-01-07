using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class JsonDataService : IDataService
{
    public bool SaveData<T>(string relativePath, T data, bool encrypted) {
        string path = Application.persistentDataPath + relativePath;
        try {
            if (File.Exists(path)) {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        } catch(Exception e) {
            Debug.LogError($"AHHHH {e.Message} {e.StackTrace}");
            return false;
        }
    }
    public T LoadData<T>(string relativePath, bool encrypted) {
        string path = Application.persistentDataPath + relativePath;
        if (!File.Exists(path)) {
            Debug.LogError("AHH WO DATEI??");
            throw new FileNotFoundException($"{path} does not exist");
        }
        try {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        } catch (Exception e) {
            Debug.LogError($"EpicFail {e.Message} {e.StackTrace}");
            throw e; 
        }
    }
}
