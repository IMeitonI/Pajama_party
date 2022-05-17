using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolacionBarrera : MonoBehaviour
{

    [SerializeField] float tiempoDeseado;
    float tiemporeal;
    float porcentajeTiempo;
    public bool moving=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            CambiarMoving();

        } 
       // if (moving) { MoverNuevaPos()}
    }
    public void MoverNuevaPos(Transform[] limite, Vector3[] nuevasPos)
    {
        tiemporeal += Time.deltaTime;
        porcentajeTiempo = tiemporeal / tiempoDeseado;
        for (int i = 0; i < limite.Length; i++)
        {
            limite[i].position = Vector3.Lerp(limite[i].position, nuevasPos[i], Mathf.SmoothStep(0, 1, porcentajeTiempo));
        }
      //  transform.localPosition = Vector3.Lerp(transform.localPosition, pp.localPosition, Mathf.SmoothStep(0, 1, porcentajeTiempo));
    }
    public void CambiarMoving()
    {
        moving = moving ? false :  true;
        print("moviendome kokook" + moving);
    }
}
