using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioMapaBarrera : MonoBehaviour
{
    [SerializeField]Map_Manager mapMana;
    [SerializeField] InterpolacionBarrera[] inter;

    public void CambiarUbicacionBarrera() {
        Invoke("TrueEnMapa", 3);
        for (int i = 0; i < inter.Length; i++) {
            inter[i].IrAPosInicial();
        }
        Movement.freezed = false;
        Movement.multiplier_speed = 1;
       
        mapMana.DisableCanvas();
        transform.SetParent(mapMana.maps[mapMana.current_map].transform);
        transform.localPosition=mapMana.maps[mapMana.current_map].transform.GetChild(0).transform.localPosition;
    }
    public void TrueEnMapa() {
        TiempoParaBarrera.enMapa = true;
    }
}
