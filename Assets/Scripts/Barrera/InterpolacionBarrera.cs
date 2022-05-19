using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolacionBarrera : MonoBehaviour {

    [SerializeField] float tiempoDeseado=5000;
    Vector3 miNuevaPos;
    float tiemporeal;
    float porcentajeTiempo;
    [SerializeField]Transform miPosInicial;

    private void Start() {
       // miPosInicial = transform;
    }
    void Update() {
        
        if (TiempoParaBarrera.activarBarrera == true) { MoverNuevaPos(); }
    }
    public void MoverNuevaPos() {
        tiemporeal += Time.deltaTime;
        porcentajeTiempo = tiemporeal / tiempoDeseado;
        transform.position = Vector3.Lerp(transform.position, miNuevaPos, porcentajeTiempo);
        //  transform.localPosition = Vector3.Lerp(transform.localPosition, pp.localPosition, Mathf.SmoothStep(0, 1, porcentajeTiempo));
    }

    public void AsignarNuevaPos(Vector3 nuevaPos) {
        miNuevaPos = nuevaPos;
    }

    public void IrAPosInicial() {
        transform.position = miPosInicial.position;
    }
  
}
