using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadSaveNickname : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { Debug.Log("si lo estoy haceido");
        
    }

    // Update is called once per frame
    public void SaveNickname()
    {
        Save_Manager.saveM_instance.activeSave.nickname = PhotonNetwork.NickName;
        Save_Manager.saveM_instance.Save();
    }
}
