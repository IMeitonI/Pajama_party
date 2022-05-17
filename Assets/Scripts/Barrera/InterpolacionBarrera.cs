using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolacionBarrera : MonoBehaviour {

    [SerializeField] float tiempoDeseado;
    Vector3 miNuevaPos;
    float tiemporeal;
    float porcentajeTiempo;
    public bool moving = false;

   
    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.A)) {
            CambiarMoving();

        }
        if (moving) { MoverNuevaPos(); }
    }
    public void MoverNuevaPos() {
        tiemporeal += Time.deltaTime;
        porcentajeTiempo = tiemporeal / tiempoDeseado;


        transform.position = Vector3.Lerp(transform.position, miNuevaPos, Mathf.SmoothStep(0, 1, porcentajeTiempo));

        //  transform.localPosition = Vector3.Lerp(transform.localPosition, pp.localPosition, Mathf.SmoothStep(0, 1, porcentajeTiempo));
    }
    public void CambiarMoving() {
        moving = moving ? false : true;
        print("moviendome kokook" + moving);
    }

    public void AsignarNuevaPos(Vector3 nuevaPos) {
        miNuevaPos = nuevaPos;
    }
}
