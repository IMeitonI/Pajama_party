using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Save_Manager : MonoBehaviour {
    public static Save_Manager saveM_instance;
    [SerializeField] SkinData skinOnline, skinPlayer1, skinPlayer2;

    public SaveData activeSave;
    public bool loaded;

    private void Awake() {
        if (saveM_instance == null) {
            saveM_instance = this;
        } else if (saveM_instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Load();
    }

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

    public void Save() {
        
        //if (activeSave.online) {
        
            activeSave.onlineCharacter =skinOnline.SaveCharacter();
        //} else {
            activeSave.character_1 = skinPlayer1.SaveCharacter();
            activeSave.character_2 = skinPlayer2.SaveCharacter();
        //}

        string json = JsonUtility.ToJson(activeSave);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
        Debug.Log("Guardado: " + json);
    }
    public void Load() {

        if (File.Exists(Application.dataPath + "/save.txt")) {

            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
            activeSave.nickname = saveData.nickname;
            //activeSave.online = saveData.online;
            activeSave.muted = saveData.muted;
            activeSave.character_1 = saveData.character_1;

            activeSave.character_2 = saveData.character_2;

            activeSave.onlineCharacter = saveData.onlineCharacter;
            //if (activeSave.online) {
                skinOnline.LoadCharacter(activeSave.onlineCharacter);
            //} else {
                skinPlayer1.LoadCharacter(activeSave.character_1);
                skinPlayer2.LoadCharacter(activeSave.character_2);
            //}


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
    //public bool online;
    public string nickname;
    public bool muted;
    public int[] character_1 = new int[4];
    public int[] character_2 = new int[4];
    public int[] onlineCharacter = new int[4];
    //public ScriptableObject onlineCharacter;

}
