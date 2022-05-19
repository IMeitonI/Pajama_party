using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class BoomerangLauncher : MonoBehaviour
{
    [SerializeField] float throwForce;
    [SerializeField] float speedRotation;
    [SerializeField] private BoomerangLogic boomerangRef;
    public GameObject ButtonMagnet;
    public bool isReturning;

    [SerializeField]Player2_Boomerang boomerangCheckColicions;


    public void ThrowBomerang()
    {
        if (boomerangRef.IsWithPlayer() && !boomerangRef.canReturn&&boomerangCheckColicions.alive)
        {
            ThrowBtn();
            ButtonMagnet.SetActive(true);
        }
    }

    public void ReturnBoomerang()
    {
        if (boomerangRef.canReturn&&boomerangCheckColicions.alive)
        {
            boomerangRef.ReCall();
            isReturning=true;

        }
    }

    public void GiveBoomerang()
    {
        if (boomerangRef.canReturn&&boomerangCheckColicions.alive)
        {
            boomerangRef.GiveBoomerang();
            isReturning=false;
        }
    }

    public void RotatePlayer()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speedRotation * Time.deltaTime, transform.eulerAngles.z);
    }
    void Update()
    {
        if(isReturning)
        {
            boomerangRef.ReCall();
        }

        // if (Input.GetAxis("Fire1") > 0)
        // {

        //     RotatePlayer();
        // }


        // if (Input.GetMouseButtonDown(1))
        // {

        //     ThrowBomerang();
        // }

        // if (Input.GetAxis("Fire1") > 0)
        // {

        //     ReturnBoomerang();
        // }
        // else if (Input.GetButtonUp("Fire1"))
        // {
        //     GiveBoomerang();
        // }

        // else if(Input.GetMouseButtonUp(1))
        // {
        //     boomerangRef.Nothing();

        // }
    }

    public Vector3 GetPosition()
    {
        Vector3 pos = this.gameObject.transform.position;
        pos.y = 0.34f;
        return pos;
    }

    public void ThrowBtn()
    {
        Vector3 throwDir = transform.forward;
        boomerangRef.ThrowBoomerang(throwDir, throwForce);
    }
}
