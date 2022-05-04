using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DashOnline : MonoBehaviour, IPunObservable
{
    [SerializeField] private float dash_time, dash_speed, initial_speed, cooldown, current_time;
    Transform player;
    protected bool dash_enable;

    Button btn;
    MovementOnline mov;
    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(dash_enable);
            stream.SendNext(transform.position);
        }
        else
        {
            this.dash_enable = (bool)stream.ReceiveNext();
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.FindGameObjectWithTag("Dash").GetComponent<Button>();
        btn.onClick.AddListener(Star_Dash);
        if (pv.IsMine)
        {
            player = transform.GetChild(2).GetComponent<Transform>();
            mov = GetComponent<MovementOnline>();
            current_time = cooldown;
        }

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
            Vector3 target_pos = transform.position + player.forward * dash_speed * Time.deltaTime;
            target_pos = new Vector3(target_pos.x, transform.position.y, target_pos.z);
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, player.forward * dash_speed * Time.deltaTime, out raycastHit, 2f);
            if (raycastHit.collider == null)
            {
                //Can move
                transform.position = target_pos;
            }
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
