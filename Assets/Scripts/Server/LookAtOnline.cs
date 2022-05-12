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
        pv = GetComponentInParent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {

        managerJoystick = GameObject.FindGameObjectWithTag("MyJoy").GetComponent<ManagerJoystick>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (managerJoystick == null) return;
        transform.position = new Vector3(0, -1, 0);
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
            stream.SendNext(player.rotation);
        }
        else
        {
            this.player.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
