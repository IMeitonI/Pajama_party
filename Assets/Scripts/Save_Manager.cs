using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Save_Manager : MonoBehaviour {
    public static Save_Manager saveManager;
    public SaveData activeSave;
    public bool loaded;

    private void Awake() {
        saveManager = this;
        Load();


    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Save();
            print("guardado");
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            Load();
            print("Cargando");
        }
        if (Input.GetKeyDown(KeyCode.C)) {
             DeleteData();
        }
    }

    //public bool IsSavefile() {
    //    return Directory.Exists(Application.persistentDataPath + "/save");
    //}
    //public void Save() {
    //    if (!IsSavefile()) {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/save");
    //    }
    //    if(!Directory.Exists(Application.persistentDataPath + "/save/character_data")){
    //        Directory.CreateDirectory(Application.persistentDataPath + "/save/character_data");
    //    }

    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/save/character_data/character_save.txt");
    //    var json = JsonUtility.ToJson(prueba);
    //    print(json.ToString());
    //    bf.Serialize(file, json);
    //    file.Close();

    //}
    //public void Load() {
    //    if (!Directory.Exists(Application.persistentDataPath + "/save/character_data")) {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/save/character_data");
    //    }
    //    BinaryFormatter bf = new BinaryFormatter();
    //    if(File.Exists(Application.persistentDataPath + "/save/character_data/character_save.txt")) {
    //        FileStream file = File.Open(Application.persistentDataPath + "/save/character_data/character_save.txt", FileMode.Open);
    //        JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), prueba);
    //        file.Close();
    //    }
    //}
    public void Save() {

       
        string json = JsonUtility.ToJson(activeSave);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
        Debug.Log("Guardado: " + json);
    }
    public void Load() {


        if (File.Exists(Application.dataPath + "/save.txt")) {

            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
     
                activeSave.character_1 = saveData.character_1;
  
                activeSave.character_2 = saveData.character_2;
  
                activeSave.onlineCharacter = saveData.onlineCharacter;
            
           
            loaded = true;

            Debug.Log("Cargado: " + saveString);
        }
    }
    public void DeleteData() {
        if (File.Exists(Application.dataPath + "/save.txt")) {
            File.Delete(Application.dataPath + "/save.txt");
            File.Delete(Application.dataPath + "/save.txt.meta");
            Debug.Log("Lo borré");
        }
    }
}
[System.Serializable]
public class SaveData {
    // public Online_skin skin;
    //public int guardados;
    public bool online;
    public int[] character_1 = new int[4];
    public int[] character_2 = new int[4];
    public int[] onlineCharacter = new int[4];
    //public ScriptableObject onlineCharacter;

}
