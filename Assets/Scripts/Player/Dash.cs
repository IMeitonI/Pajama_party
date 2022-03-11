using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash : MonoBehaviour
{
    [SerializeField] private float dash_time, dash_speed, initial_speed, cooldown, current_time;
    Transform player;
    Movement mov;
    private bool dash_enable;

    private void Start()
    {
        player = GetComponent<Transform>();
        mov = GetComponent<Movement>();
        current_time = cooldown;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current_time >= cooldown) dash_enable = true;
        else current_time += Time.deltaTime;
        /*if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Dash_coroutine());
        }*/
    }
    IEnumerator Dash_coroutine()
    {
        float start_time = Time.time;
        while (Time.time < start_time + dash_time)
        {
            player.position += player.forward * dash_speed*Time.deltaTime;
            yield return null;
        }

    }
    public void Star_Dash()
    {
        if (dash_enable)
        {
            current_time = 0;
            dash_enable = false;
            StartCoroutine(Dash_coroutine());
        }
  
    }
}
