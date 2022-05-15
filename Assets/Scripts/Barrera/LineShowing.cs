using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShowing : MonoBehaviour
{
    [SerializeField] public Transform[] points;
    [SerializeField] private LineController lineController;
    [SerializeField] GeneradorPosicion generador;
    // Start is called before the first frame update
    void Start()
    {
        lineController.SetUpLine(points);
        generador.MiNuevaPosicionCentral(points);
    }
   


}
