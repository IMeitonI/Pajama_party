using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class LoadUIScoreImage : MonoBehaviour {
    Image myImage;
    [Tooltip("Numero de jugador. Ej: jugador 1 poner un 1 en este campo")][SerializeReference] int player;
    private void Awake() {
        myImage = GetComponent<Image>();
    }
    void Start() {
        LoadImage();

    }

    void LoadImage() {
        switch (player) {
            case 1:
                myImage.sprite = ImageCharacterConteiner.instance.GetSprite(Save_Manager.saveM_instance.activeSave.character_1[0]);
                break;
            case 2:
                myImage.sprite = ImageCharacterConteiner.instance.GetSprite(Save_Manager.saveM_instance.activeSave.character_2[0]);
                break;         
        }
    }

   public void InstanceOnlineImage(Player player)
    {
        myImage.sprite = ImageCharacterConteiner.instance.GetSprite((int)player.CustomProperties["Face"]);
       
    }
}
