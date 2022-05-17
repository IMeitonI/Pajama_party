using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class BoomerangLauncher : MonoBehaviour
{
    [SerializeField] float throwForce;
    [SerializeField] float speedRotation;
    [SerializeField] private BoomerangLogic boomerangRef;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speedRotation * Time.deltaTime, transform.eulerAngles.z);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (boomerangRef.IsWithPlayer())
            {

                ThrowBtn();
            }
            else
            {
                boomerangRef.ReCall();
            }
        }
        // else if(Input.GetMouseButtonUp(1))
        // {
        //     boomerangRef.Nothing();

        // }
    }

    public Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    public void ThrowBtn()
    {
        Vector3 throwDir = transform.forward;
        boomerangRef.ThrowBoomerang(throwDir, throwForce);
    }
}
