using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody rb;
    Vector3 shootPos;
    [SerializeField]Transform originalPos, maxPos;
    Transform pos ;
    bool returnB=false;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        pos = GetComponentInParent<Transform>();
       // originalPos = GetComponentInParent<Transform>().position;
       
    }
    void Start()
    {
        
        shoot();
        
    }

   
    void Update() {

        GoandReturn();
      


    }
   

    void shoot() {

      
        pos.SetParent(null, true);
        shootPos=transform.position;
        rb.AddForce(transform.forward * speed);

    }

    void GoandReturn() {
       
        if (returnB==false && Vector3.Distance(maxPos.position,transform.position) <= 10 ) {
            //rb.AddForce(-transform.forward * speed);
            rb.velocity = Vector3.zero;          
            Debug.Log("A");
            returnB = true;
        } else if (returnB) {
            Follow();
        }
        if (transform.position.z <= shootPos.z) {
           // rb.velocity = Vector3.zero;
        }
    }
    void Follow() {
        Vector3 dir = originalPos.position-transform.position;
        
        Debug.Log(dir);
        // transform.position += (transform.position - originalPos * -speed) * Time.deltaTime;
        rb.MovePosition(transform.position + (dir *speed* Time.deltaTime));
       
       // rb.AddForce (dir * speed* Time.deltaTime);
    }



}
