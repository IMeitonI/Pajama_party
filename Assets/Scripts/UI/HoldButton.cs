using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour
{
    Movement mov;
  

    public void Aim(bool x)
    {
        mov = GetComponent<Movement>();
        mov.Aim(x);
    }

}
