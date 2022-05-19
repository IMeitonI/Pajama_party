using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TiempoParaBarrera : MonoBehaviour
{
    [SerializeField] LineController lineController;
    [SerializeField] GameObject amarillo;
    [SerializeField]float tiempoBarrera = 60;
    float tiempoReal;
    bool advertenciaActiva =false;
    float advertenciaAmarilla;
    static public bool activarBarrera = false;
    bool enMapa = true;


    private void Start() {
        advertenciaAmarilla = tiempoBarrera * 3 / 4;
    }
    void Update()
    {
        if (enMapa) {

            tiempoReal += Time.deltaTime;

            if (tiempoReal >= advertenciaAmarilla && !advertenciaActiva) {
                amarillo.SetActive(true);
                advertenciaActiva= true;
                Invoke("DesactivarAdver", 2f);
            }
            if (tiempoReal >= tiempoBarrera) {
                activarBarrera = true;
                lineController.Activar();
                tiempoReal = 0;
            }

        }
    }
    void DesactivarAdver() {
     
        amarillo.SetActive(false);
    }
}
