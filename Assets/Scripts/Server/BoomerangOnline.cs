using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class BoomerangOnline : Player2_Boomerang,IPunObservable
{
    Button btn;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myBoomerang.shooted);
        }
        else
        {
            this.myBoomerang.shooted = (bool)stream.ReceiveNext();
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myBoomerang.DeactiveColider += DeactivateCol;
        //map_Manager = FindObjectOfType<Map_Manager>();
        myCollider = GetComponent<CapsuleCollider>();
        //alive = true;
        myBoomerang.target = transform;
        mov = GetComponent<Movement>();
        //map_Manager.Mapchanger += Activatecollider;
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.FindGameObjectWithTag("Shoot").GetComponent<Button>();
        //btn.OnPointerDown()
        btn.onClick.AddListener(Shoot);
    }

}
