using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundCheckl : MonoBehaviour
{
    public bool grounded;
    RaycastHit[] hits = new RaycastHit[4];
    Vector3[] array = new Vector3[4];
    // Start is called before the first frame update
    private void Start()
    {
        array[0] = new Vector3(-1.5f,0,0);
        array[1] = new Vector3(1.5f, 0, 0);
        array[2] = new Vector3(0, 0, -1.5f);
        array[3] = new Vector3(0, 0, 1.5f);
        grounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = CheckGround();
        Debug.Log(hits[0].collider.gameObject.tag);
    }
    public bool CheckGround()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            Physics.Raycast(transform.position + array[i], Vector3.down, out hits[i], 20f);
            if (hits[i].collider != null && hits[i].collider.CompareTag("Ground")) return true;
        }
        return  false;
    }
}
