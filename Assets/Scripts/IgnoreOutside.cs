using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreOutside : MonoBehaviour
{
    Test_boomerang boomerang;
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Boomerang") ) {
            boomerang = other.GetComponent<Test_boomerang>();
            if (boomerang.speed == 0) boomerang.PickUp();

        }
    }
}
