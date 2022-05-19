using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public delegate void AnimatorEvents();
    public event AnimatorEvents Fall;
    Animator anim;
    Movement mov;
    [Range(0, 1)]
    [SerializeField] int animation_type;
    Dash dash;
    LookAt look;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Map_Manager.Mapchanger += Disable;
    }
    private void OnDisable()
    {
        Map_Manager.Mapchanger -= Disable;
    }
    void Start()
    {
        mov = GetComponent<Movement>();
        dash = GetComponent<Dash>();
        anim = GetComponent<Animator>();
        look = GetComponent<LookAt>();
        anim.SetInteger("Animation_type", animation_type);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mov.running) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
        if (mov.falling) Falling();
        //if(dash)
    }
    public void Throw_Anim()
    {
        anim.SetTrigger("Throw");
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        mov.die = true;
        look.death = true;
        if (Map_Manager.players_deaths == 0)
        {         
            Map_Manager.players_deaths++;
            Invoke("Disable", 2f);
        }
  
    }
    void Disable()
    {
        if(gameObject != null)gameObject.SetActive(false);
        if (anim.GetBool("Falling")) anim.SetBool("Falling", false);
        if (Mov_Camera.local && Map_Manager.players_deaths ==1)
        {
            Map_Manager.players_deaths = 0;
            Map_Manager.change_mp = true;
            Invoke("Change_Map", 5);
        }
        //else ++Map_Manager.players_deaths;

    }
    void Change_Map()
    {
        mov.die = false;
        mov.falling = false;
        look.death = false;
        gameObject.SetActive(true);
        anim.SetBool("Falling", false);
        anim.SetInteger("Animation_type", animation_type);
        Map_Manager.change_mp = false;
    }
    public void Falling()
    {
        if (Fall != null) Fall();
        anim.SetBool("Falling", true);
        mov.die = true;
        if (Map_Manager.players_deaths == 0)
        {
            Map_Manager.players_deaths++;
            Invoke("Disable", 2f);
        }
    }
    public void Dash()
    {
        anim.SetTrigger("Dash");
    }

}
