using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    [SerializeField] MovimientoObjeto MovimientoObjeto;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        MovimientoObjeto.CambiaTranform();
           
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            MovimientoObjeto.CambiaTranform();
            
        }
    }
}
