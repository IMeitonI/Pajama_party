using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_System : MonoBehaviour
{
   [SerializeField] Customise_Manager customise_Manager;
    [SerializeField] Launcher launcher;
    [SerializeField] Online_skin skin;
    void Start() {
        customise_Manager.SaveT += Transformation;
        launcher.GameStart +=LoadCharacter;
    }

    public void Transformation(int player,Online_skin skin) {
        switch (player) {
            case 1: Save_Manager.saveManager.activeSave.character_1 = skin.SaveCharacter();
                break;
            case 2:
                Save_Manager.saveManager.activeSave.character_2 = skin.SaveCharacter();
                break;
            case 3:
                Save_Manager.saveManager.activeSave.onlineCharacter = skin.SaveCharacter();
                break;
        }
        Save_Manager.saveManager.Save();
    }
     
    public void LoadCharacter() {
        Save_Manager.saveManager.Load();
       
        
    }
}
