using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LookAtOnline : LookAt, IPunObservable
{
    PhotonView pv;

    // Start is called before the first frame update
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            return;
        }
        managerJoystick = GameObject.FindGameObjectWithTag("MyJoy").GetComponent<ManagerJoystick>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (managerJoystick == null) return;
        direction.x = managerJoystick.InputHorizontal();
        direction.y = managerJoystick.InputVertical();
        if (direction != Vector2.zero && death == false)
        {
            float angle = Mathf.Atan2(direction.y - Vector2.zero.y, direction.x - Vector2.zero.x);
            player.rotation = Quaternion.Euler(0f, 90 - angle * Mathf.Rad2Deg, 0f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
