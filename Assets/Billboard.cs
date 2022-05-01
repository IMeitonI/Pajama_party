using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;
    Transform poder;
    private void Update()
    {
        if(cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
        if(cam==null)
        {
            return;
        }
       // transform.LookAt(cam.transform);
      

        transform.eulerAngles = new Vector3(90, 0, 0f);
        transform.Rotate(Vector3.right * 180);

    }
}
