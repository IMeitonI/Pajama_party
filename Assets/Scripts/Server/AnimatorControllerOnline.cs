using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class AnimatorControllerOnline : MonoBehaviour
{
    public delegate void AnimatorEvents();
    public event AnimatorEvents Fall;
    Animator anim;
    MovementOnline mov;
    [Range(0, 1)]
    [SerializeField] int animation_type;
    LookAtOnline look;
    Button shoot_bttn;

    PhotonView pv;
    // Start is called before the first frame update
    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
    }
    void Start()
    {
        mov = GetComponentInParent<MovementOnline>();
        anim = GetComponent<Animator>();
        look = GetComponent<LookAtOnline>();
        anim.SetInteger("Animation_type", 1);
        shoot_bttn = GameObject.FindGameObjectWithTag("Shoot").GetComponent<Button>();
        shoot_bttn.onClick.AddListener(Throw_Anim);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (mov.running) anim.SetBool("Running", true);
            else anim.SetBool("Running", false);
        }
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
        if (other.gameObject.name == "Water")
        {
            if (Fall != null) Fall();
            anim.SetBool("Falling", true);
            mov.die = true;
            Invoke("Disable", 2f);
        }
    }

}
