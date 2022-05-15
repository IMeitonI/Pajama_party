using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour
{
    Movement mov;
    [SerializeField] GameObject aim_arrow;
    float time;
    float duration = 0.2f;
    bool hold;
    float duration_anim;
  

    public void Aim(bool x)
    {
        mov = GetComponent<Movement>();
        if (x == true) hold = true;
        else
        {
            time = 0;
            hold = false;
            aim_arrow.SetActive(x);
            mov.Aim(x);
        }
    }
    private void Update()
    {
        if (hold == true) time += Time.deltaTime;
        if (time >= duration)
        {
            aim_arrow.SetActive(true);
            mov.Aim(true);
        }
    }

}
