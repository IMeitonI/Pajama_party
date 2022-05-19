using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorBarrera : MonoBehaviour
{
    [SerializeField] GameObject miRedWarning;
    [SerializeField] Score miScore;
    

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Barrier")&& Movement.freezed==false) {
            miRedWarning.SetActive(true);
            Movement.freezed = true;
            miScore.SubsScore();
            TiempoParaBarrera.enMapa = false;
            TiempoParaBarrera.activarBarrera = false;
            Invoke("CambiarMapa", 2);
        }
    }
    void CambiarMapa() {
       
       
        miRedWarning.SetActive(false);
        Movement.freezed = false;
        Map_Manager.change_mp = true;
    }
}
