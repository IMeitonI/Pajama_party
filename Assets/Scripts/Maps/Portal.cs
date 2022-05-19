using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otroPortal;
    public Transform miLugar;
    GameObject jugador;
    bool teleporting = false;
    private void OnTriggerEnter(Collider other)
    {
        print("primera fase");
        jugador = other.gameObject;
        if (jugador.CompareTag("Player") || jugador.CompareTag("Boomerang"));
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
        Quaternion ttt = Quaternion.Inverse(transform.localRotation) * jugador.transform.localRotation;
        jugador.transform.localEulerAngles = Vector3.up * (otroPortal.transform.localEulerAngles.y - (transform.localEulerAngles.y - jugador.transform.localEulerAngles.y) + 180);      
        jugador.transform.position = otroPortal.miLugar.position;


        teleporting = true;
    }
}
