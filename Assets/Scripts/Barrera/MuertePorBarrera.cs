using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorBarrera : MonoBehaviour
{
    [SerializeField] GameObject miRedWarning;
   

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Barrier")) {
            print("Hola mamasita");
            miRedWarning.SetActive(true);
            Movement.freezed = true;
            print("Como estoy: " + Movement.freezed);
           
            Invoke("CambiarMapa", 2);
        }
    }
    void CambiarMapa() {
        miRedWarning.SetActive(false);
        Movement.freezed = false;
        Map_Manager.change_mp = true;
    }
}
