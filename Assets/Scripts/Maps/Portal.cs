using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otroPortal;
    public Transform miLugar;
    GameObject jugador;
    bool teleporting = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("primera fase");
        jugador = other.gameObject;
        if (jugador.CompareTag("Player"))
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
    {
        print("Movioendome");
        print(otroPortal.miLugar.position);
        jugador.transform.position = otroPortal.miLugar.position;
        teleporting = true;
    }
}