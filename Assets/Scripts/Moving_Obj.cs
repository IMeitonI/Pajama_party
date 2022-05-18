using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Obj : MonoBehaviour
{
    [SerializeField]
    Transform movingObject;
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(movingObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
