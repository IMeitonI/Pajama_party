using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundCheckl : MonoBehaviour
{
    public static bool grounded;
    RaycastHit rhit;
    // Start is called before the first frame update
    private void Start()
    {
        Physics.Raycast(this.transform.position, Vector3.down, out rhit, 4);
    }
    // Update is called once per frame
    void Update()
    {
        grounded = CheckGround();
    }
    public bool CheckGround()
    {
        return rhit.collider != null;
    }
}
