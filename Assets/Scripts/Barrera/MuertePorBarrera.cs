using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorBarrera : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Barrier")) {
            print("Hola mamasita");
            Map_Manager.change_mp = true;
        }
    }
}
