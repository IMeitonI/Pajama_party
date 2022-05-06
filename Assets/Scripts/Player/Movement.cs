using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public static Action A_Move;
    [Header("Variables Movimiento")]
    [SerializeField] public float speed;
    [Range(-1,1)]
    [SerializeField]protected float x_axis, z_axis;
    [SerializeField] protected ManagerJoystick manager_Joystick;
    public bool running, die;
    [SerializeField] protected LayerMask layermask_check;
    public static bool tpactive = false;
    //Modificaciones Chelo
    [SerializeField] protected GameObject Shield;
    [Header("VFX")]
    [SerializeField] protected ParticleSystem ShieldPS;
    [SerializeField] protected ParticleSystem teleportPS;
    [SerializeField] protected ParticleSystem movement_trail;
    public Button TeleportButton;

    public float shieldtime = 5f;
    public bool shieldActive = false;
    protected bool firsttime = true;
    [HideInInspector] public bool teleportPU = false;

    public GameObject playerBoomerang;
    [SerializeField] public Test_boomerang myBoomerang;
    [SerializeField] AudioClip MovimientoSound;

    private float teleportTimer = 0f;

    //[Header("Sounds")]
    //public AudioClip moveSound;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager_Joystick == null) return;
        x_axis = manager_Joystick.InputHorizontal();
        z_axis = manager_Joystick.InputVertical();
        if (x_axis != 0 || z_axis != 0)
        {
            if (die == false) Change_Pos(x_axis, z_axis);
            
            movement_trail.Play();

        }
        else {
            running = false;
            
            movement_trail.Stop();
            managerSound.Instance.Play(MovimientoSound);
        }
    }
    private void Update()
    {
        if (shieldActive)
        {
            if (firsttime)
            {
                ShieldPowerUp();
                firsttime = false;
            }
        }
        if (!shieldActive)
        {
            StopShield();
        }

        if (myBoomerang.shooted)
        {
            teleportTimer += Time.deltaTime;
        }


        //Modificaci n Jose 
        //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();

        if (running == true)
        {
            //managerSound.Instance.Play(moveSound);
        }
    }
    public void Change_Pos(float x, float z)
    {
        running = true;
        Vector3 force = new Vector3(x, 0, z);
        Vector3 target_pos = transform.position + force * speed * Time.deltaTime;
        target_pos = new Vector3(target_pos.x, transform.position.y, target_pos.z);
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, force, out raycastHit, 4*speed* Time.deltaTime);
        if (raycastHit.collider == null && Map_Manager.change_mp == false)
        {
            transform.position = target_pos;
        }
        else
        {
            //Cannot move horizontally
            Vector3 second_move_dir = new Vector3(0, 0, z);
            target_pos = transform.position + second_move_dir * speed * Time.deltaTime;
            Physics.Raycast(transform.position, second_move_dir, out raycastHit, 4 * speed * Time.deltaTime);
            if (raycastHit.collider == null && Map_Manager.change_mp == false)
            {
                transform.position = target_pos;
            }
            else
            {
                //Cannot move vertically
                Vector3 third_move_dir = new Vector3(x, 0, 0);
                target_pos = transform.position + third_move_dir * speed * Time.deltaTime;
                Physics.Raycast(transform.position, third_move_dir, out raycastHit, 4 * speed * Time.deltaTime);
                if (raycastHit.collider == null && Map_Manager.change_mp == false)
                {
                    transform.position = target_pos;
                }
                else
                {
                    //Cannot move
                }
            }
        }

        A_Move?.Invoke();
    }
    public IEnumerator SpeedPowerUp()
    {
        speed = speed * 1.4f;
        yield return new WaitForSeconds(5f);
        speed = speed / 1.4f;
    }

    public void TeleportPowerUp()
    {
        if (teleportPU)
        {
            if (myBoomerang.shooted)
            {
                
                if(playerBoomerang.transform.position.z > this.transform.position.z + 2 || playerBoomerang.transform.position.z < this.transform.position.z - 2 || playerBoomerang.transform.position.x > this.transform.position.x + 5 || playerBoomerang.transform.position.x < this.transform.position.x - 5)
                {
                    if(teleportTimer >= 1)
                    {
                        tpactive = true;
                        transform.position = playerBoomerang.transform.position;
                        Instantiate(teleportPS, transform.position, Quaternion.identity);
                        teleportPS.gameObject.SetActive(true);
                        teleportPS.Play();
                        teleportTimer = 0f;
                    }
                }
            }
        }
    }

    public void ShieldPowerUp()
    {
        Shield.SetActive(true);
        ShieldPS.gameObject.SetActive(true);
        ShieldPS.Play();
    }
    public void StopShield()
    {
        Shield.SetActive(false);
        ShieldPS.Stop();
    }
}
