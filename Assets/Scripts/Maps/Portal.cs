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
                Teletransportacion(jugador);
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
    void Teletransportacion(GameObject jug)
    {// Vector3 fromPortal = transform.InverseTransformPoint(jugador.);
        print("Movioendome");
        print(otroPortal.miLugar.position);
        jugador.transform.position = otroPortal.miLugar.position;
        


        teleporting = true;
    }
}
