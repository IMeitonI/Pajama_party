using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class BoomerangOnline : Player2_Boomerang, IPunObservable
{
    Button btn;
    bool active_col;
    LookAtOnline look_online;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myBoomerang.shooted);
        }
        else
        {
            this.myBoomerang.shooted = (bool)stream.ReceiveNext();
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myBoomerang.DeactiveColider += DeactivateCol;
        //map_Manager = FindObjectOfType<Map_Manager>();
        myCollider = GetComponent<CapsuleCollider>();
        //alive = true;
        myBoomerang.target = transform;
        mov = GetComponent<Movement>();
        //map_Manager.Mapchanger += Activatecollider;
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.FindGameObjectWithTag("Shoot").GetComponent<Button>();
        //btn.OnPointerDown()
        btn.onClick.AddListener(Shoot);
        look_online = GetComponentInChildren<LookAtOnline>();
    }
    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != myBoomerang.gameObject && other.gameObject.CompareTag("Boomerang"))
        {
            if (mov.shieldActive)
            {
                mov.shieldActive = false;
                return;
            }
            else
            {
                if (other.gameObject.GetComponent<Test_boomerang>().speed == 0) return;

                DeactivateCol();
                AnimatorControllerOnline animOn = GetComponentInChildren<AnimatorControllerOnline>();
                animOn.Die();
                MapManagerOnline.players_deaths++;

                alive = false;
                // Modificaci n Jose
                //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
                managerSound.Instance.Play(DieSound);
                //Hasta ac  

            }

        }
        
    }
    private void Update()
    {
        if (MapManagerOnline.changing_mp == false && active_col ==false) Activatecollider();
    }
    protected void DeactivateCol()
    {
        Debug.Log("DEACTIVO");
        //rb.useGravity = false;
        rb.isKinematic = true;
        myCollider.enabled = false;
        active_col = false;
    }
    protected void Activatecollider()
    {
        Debug.Log("ACTIVO");
        //rb.useGravity = true;
        myCollider.enabled = true;
        rb.isKinematic = false;
        active_col = true;
        look_online.death = false;
        look_online.gameObject.SetActive(true);
        mov.die = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Water")
        {
            DeactivateCol();
            AnimatorControllerOnline animOn = GetComponentInChildren<AnimatorControllerOnline>();
            animOn.Die();
            MapManagerOnline.players_deaths++;

            alive = false;
            // Modificaci n Jose
            //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
            managerSound.Instance.Play(DieSound);
            //Hasta ac  
            //Hasta ac  
        }
    }

}
