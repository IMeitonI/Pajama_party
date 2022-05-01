using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoomerangOnline : Player2_Boomerang,IPunObservable
{
    bool m_isFiring;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_isFiring);
        }
        else
        {
            this.m_isFiring = (bool)stream.ReceiveNext();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
