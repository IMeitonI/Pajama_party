using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public delegate void AnimatorEvents();
    public event AnimatorEvents Fall;
    Animator anim;
    Movement mov;
    [Range(0,1)]
    [SerializeField] int animation_type;
    LookAt look;
    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        look = GetComponent<LookAt>();
        anim.SetInteger("Animation_type", animation_type);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mov.running) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
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
        Invoke("Disable", 2f);
    }
    void Disable()
    {
        gameObject.SetActive(false);
        if (anim.GetBool("Falling")) anim.SetBool("Falling", false);
        if (Mov_Camera.local)
        {
            Map_Manager.change_mp = true;
            Invoke("Change_Map", 5);
        }
        else ++Map_Manager.players_deaths;

    }
    void Change_Map()
    {
        mov.die = false;
        look.death = false;
        gameObject.SetActive(true);
        anim.SetInteger("Animation_type", animation_type);
        Map_Manager.change_mp = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Water")
        {   if (Fall != null) Fall();
            anim.SetBool("Falling", true);
            mov.die = true;
            Invoke("Disable", 2f);
        }
    }
}
