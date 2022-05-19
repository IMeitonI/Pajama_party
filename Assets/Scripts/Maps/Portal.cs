using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otroPortal;
    public Transform miLugar;
    GameObject jugBoom;
    bool teleporting = false;
    private void OnTriggerEnter(Collider other)
    {
        print("primera fase");
        jugBoom = other.gameObject;
        if (jugBoom.CompareTag("Player") || jugBoom.CompareTag("Boomerang"));
        {
            if (teleporting == false)
            {
                Teletransportacion();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (teleporting == true)
        {
            print("teleporting FALSE");
            teleporting = false;
        }
    }
    void Teletransportacion()
    {// Vector3 fromPortal = transform.InverseTransformPoint(jugador.);
        print("Movioendome");
        Quaternion ttt = Quaternion.Inverse(transform.localRotation) * jugBoom.transform.localRotation;
        jugBoom.transform.localEulerAngles = Vector3.up * (otroPortal.transform.localEulerAngles.y - (transform.localEulerAngles.y - jugBoom.transform.localEulerAngles.y) + 180);      
        jugBoom.transform.position = otroPortal.miLugar.position;


        teleporting = true;
    }
}
