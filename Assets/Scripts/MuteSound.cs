using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    bool muted;

   
    public void ChangeMute() {
        if (muted){
            muted = false;
            AudioListener.volume = 1;
            //imagen.sprite = mute;
            print("No muteado");
        } else {
            muted = true;
            AudioListener.volume = 0;
            //imagen.sprite = volumeOn;
            print("muteado");
        }
        Save_Manager.saveM_instance.activeSave.muted = muted;
        Save_Manager.saveM_instance.Save();
        ChargeMute();

    }
    public void ChargeMute() {
        muted = Save_Manager.saveM_instance.activeSave.muted;
        if (muted){
            //imagen.sprite = mute;
            AudioListener.volume = 0;
        } else {
            AudioListener.volume = 1;
            //imagen.sprite = volumeOn;
        }
       
    }
  
}
