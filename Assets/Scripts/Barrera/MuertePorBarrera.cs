using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorBarrera : MonoBehaviour
{
    [SerializeField] GameObject miRedWarning;

    private void Start() {
        Movement.freezed = false;
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Barrier")&& Movement.freezed==false) {
            miRedWarning.SetActive(true);
            Movement.freezed = true;
            TiempoParaBarrera.enMapa = false;
            TiempoParaBarrera.activarBarrera = false;
            Invoke("CambiarMapa", 2);
        }
    }
    //private void onenable() {
    //    Map_Manager.Mapchanger += CambiarMapa;

    //}
    //private void ondisable() {
    //    Map_Manager.Mapchanger -= CambiarMapa;

    //}
    void CambiarMapa() {

        TiempoParaBarrera.enMapa = true;
        miRedWarning.SetActive(false);
        Movement.freezed = false;
      
    }
}
