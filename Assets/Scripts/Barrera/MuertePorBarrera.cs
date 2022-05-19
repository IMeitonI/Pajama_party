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
            
            Invoke("CambiarMapa", 2);
        }
    }
    void CambiarMapa() {
        
        Map_Manager.change_mp = true;
    }
}
