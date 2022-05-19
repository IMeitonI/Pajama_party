using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoParaBarrera : MonoBehaviour
{
    [SerializeField] LineController lineController;
    [SerializeField]float tiempoBarrera = 60;
    float tiempoReal;
    static public bool activarBarrera = false;
    bool enMapa = true;
   

    // Update is called once per frame
    void Update()
    {
        if (enMapa) {

            tiempoReal += Time.deltaTime;
            if (tiempoReal >= tiempoBarrera) {
                activarBarrera = true;
                lineController.Activar();
            }

        }
    }
}
