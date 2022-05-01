using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DashOnline : Dash, IPunObservable
{
    Button btn;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(dash_enable);
        }
        else
        {
            this.dash_enable = (bool)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.FindGameObjectWithTag("Dash").GetComponent<Button>();
        btn.onClick.AddListener(Star_Dash);
    }

}
