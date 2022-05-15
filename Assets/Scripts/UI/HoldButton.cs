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
    [SerializeField]Test_boomerang boomerang;
    float time, anim_time;
    float duration = 0.2f;
    bool hold;
    float duration_anim = 0.5f;

    public void Aim(bool x)
    {
        mov = GetComponent<Movement>();
        if (x == true) hold = true;
        else
        {
            time = 0;
            anim_time = 0;
            hold = false;
            aim_arrow.SetActive(x);
            mov.Aim(x);
        }
    }
    private void Update()
    {
        if (hold == true && boomerang.shooted == false)
        {
            time += Time.deltaTime;
            anim_time += Time.deltaTime;
            float temp = anim_time / duration_anim;
            aim_arrow.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(0.3f,1,0.5f), temp);
        }

        if (time >= duration)
        {
            aim_arrow.SetActive(true);
            mov.Aim(true);
        }
    }

}
