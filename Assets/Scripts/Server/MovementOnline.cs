using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MovementOnline :Movement,IPunObservable
{
    PhotonView pv;
    // Start is called before the first frame update
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        {
            if (!pv.IsMine)
            {
                Destroy(GetComponentInChildren<Camera>().gameObject);
                return;
            }
            manager_Joystick = GameObject.FindGameObjectWithTag("MyJoy").GetComponent<ManagerJoystick>();
            TeleportButton = GameObject.FindGameObjectWithTag("Shoot").GetComponent<Button>();
            TeleportButton.onClick.AddListener(TeleportPowerUp);

        }

    }

    void FixedUpdate()
    {
        if (manager_Joystick == null) return;
        x_axis = manager_Joystick.InputHorizontal();
        z_axis = manager_Joystick.InputVertical();
        if (x_axis != 0 || z_axis != 0)
        {
            if (die == false) Change_Pos(x_axis, z_axis);
            movement_trail.Play();

        }
        else
        {
            running = false;
            movement_trail.Stop();
        }
    }
    private void Update()
    {
        if (shieldActive)
        {
            if (firsttime)
            {
                ShieldPowerUp();
                firsttime = false;
            }
            shieldtime -= Time.deltaTime;
        }
        if (shieldtime <= 0)
        {
            StopShield();
            shieldActive = false;
            shieldtime = 5f;
        }

        //Modificaci n Jose 
        managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();

        if (running == true)
        {
            //manager.soundMove();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(speed);
            stream.SendNext(shieldActive);
            stream.SendNext(firsttime);
            stream.SendNext(running);
            stream.SendNext(die);
            stream.SendNext(teleportPU);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.speed = (float)stream.ReceiveNext();
            this.shieldActive = (bool)stream.ReceiveNext();
            this.firsttime = (bool)stream.ReceiveNext();
            this.running = (bool)stream.ReceiveNext();
            this.die = (bool)stream.ReceiveNext();
            this.teleportPU = (bool)stream.ReceiveNext();
        }
    }
}
