using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{
    [Tooltip("Tiempo en el que es hecha la animacion")] [SerializeField] float tiempoDeseado = 2;
    [SerializeField] Transform p1;
    [SerializeField] Transform p2;
    Transform pp;
    bool moving = false;
    float tiemporeal, porcentajeTiempo;
    // Start is called before the first frame update
    Vector3 primeraPos, segundaPos;
    private void Start()
    {
        transform.position = p1.localPosition;
        pp = p1;
    }
    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            GoForward();
        }
    }

    void GoForward()
    {
        tiemporeal += Time.deltaTime;
        porcentajeTiempo = tiemporeal / tiempoDeseado;
        transform.localPosition = Vector3.Lerp(transform.localPosition, pp.localPosition, Mathf.SmoothStep(0, 1, porcentajeTiempo));
        print("Äqui en min");
        if (porcentajeTiempo >= 1) moving = false;
    }
    
    public  void CambiaTranform()
    {
        tiemporeal = 0;
        if (pp == p1) pp = p2;
        else if (pp == p2) pp = p1;
        moving = true;

    }
}
