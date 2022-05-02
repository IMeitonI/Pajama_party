using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerPh : MonoBehaviour
{

    [SerializeField] float sprintSpeed, walkSpeed, smoothTime;

    public ManagerJoystick manager_Joystick;
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody rb;

    PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            return;
        }
        manager_Joystick = GameObject.FindGameObjectWithTag("MyJoy").GetComponent<ManagerJoystick>();


    }


    private void Update()
    {
        if (!pv.IsMine) return;

        //Move();

    }

    void Move()
    {
        Vector3 moveDir = new Vector3(manager_Joystick.InputHorizontal(), 0, manager_Joystick.InputVertical()).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        //rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

}
