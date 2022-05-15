using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPosicion : MonoBehaviour
{
    [SerializeField] public Vector3[] nuevasPos= new Vector3[4];
    InterpolacionBarrera interpolacion;
    float x, y, z;
    [SerializeField] [Tooltip(" VALOR POSITIVO.La diferencia entre las posiciones originales del linerenderer y a donde estara el campo de genaracion de " +
        "posicion central de la zona limitada")]int resta = 8;
    [SerializeField] int deCentralanuevaPos = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MiNuevaPosicionCentral(Transform[] limite) {
        //var = a==b?a:c;
        //limite[0].position.x >= 0 ? limite[0].position.x - 20 : limite[0].position.x + 20;
        x = Random.Range(limite[0].position.x >= 0 ? limite[0].position.x - resta : limite[0].position.x + resta, 
        limite[1].position.x >= 0 ? limite[1].position.x - resta : limite[1].position.x + resta);
        z= Random.Range(limite[0].position.z >= 0 ? limite[0].position.z - resta : limite[0].position.z + resta,
            limite[3].position.z >= 0 ? limite[3].position.z - resta : limite[3].position.z + resta);
        y = transform.position.y;
        transform.position = new Vector3(x, y, z);
        GeneracionNuevasPos();
    }

    public void GeneracionNuevasPos()
    {
        nuevasPos[0]= new Vector3(transform.position.x + deCentralanuevaPos, transform.position.y, transform.position.z + deCentralanuevaPos);
        nuevasPos[1] = new Vector3(-nuevasPos[0].x, transform.position.y, nuevasPos[0].z);
        nuevasPos[2] = -nuevasPos[0];
        nuevasPos[3] = new Vector3(nuevasPos[0].x, transform.position.y, -nuevasPos[0].z);
    }

    
}
