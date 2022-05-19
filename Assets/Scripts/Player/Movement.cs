using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public static Action A_Move;
    [Header("Variables Movimiento")]
    [SerializeField] public float speed;
    public static int multiplier_speed;
    [Range(-1, 1)]
    [SerializeField] protected float x_axis, z_axis;
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
    [SerializeField] protected ParticleSystem splashPS;
    static public bool freezed;
    public Button TeleportButton;

    public float shieldtime = 5f;
    public bool shieldActive = false;
    protected bool firsttime = true;
    [HideInInspector] public bool teleportPU = false;

    public GameObject playerBoomerang;
    [SerializeField] public Test_boomerang myBoomerang;
    [SerializeField] AudioClip splashSFX;

    private float teleportTimer = 0f;

    public bool firstTimeSpeed = true;

    private bool firstTimeShield = true;
    private bool isShieldActive = false;

    public bool falling, aiming;
    GroundCheckl check;
    Dash dash;
    Rigidbody rg;

    public bool firstTimeFalling = true;

    //[Header("Sounds")]
    //public AudioClip moveSound;
    // Update is called once per frame
    private void Start()
    {
        check = GetComponent<GroundCheckl>();
        dash = GetComponent<Dash>();
        rg = GetComponent<Rigidbody>();
        multiplier_speed = 1;
    }
    void FixedUpdate()
    {
        if (manager_Joystick == null) return;
        x_axis = manager_Joystick.InputHorizontal();
        z_axis = manager_Joystick.InputVertical();
        if (x_axis != 0 || z_axis != 0)
        {
            if (die == false) Change_Pos(x_axis, z_axis);

            movement_trail.Play();
            //managerSound.Instance.Play(MovimientoSound);

        }
        else
        {
            running = false;
            movement_trail.Stop();
        }

     
    }
    private void OnEnable()
    {
        Map_Manager.Mapchanger += SetConstraints;
        die = false;
        falling = false;
        firstTimeFalling = true;
        multiplier_speed = 1;
        Collider temp = GetComponent<Collider>();
        temp.enabled = true;
        if (check != null)check.grounded = true;
    }
    private void OnDisable()
    {
        transform.parent = null;
        Map_Manager.Mapchanger -= SetConstraints;
    }
    void SetConstraints()
    {
        die = false;
        falling = false;
        firstTimeFalling = true;
        multiplier_speed = 1;
        if (check != null) check.grounded = true;
        rg.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
    private void Update()
    {

        if (check.grounded == false && dash.dash_used == false)
        {
            if (!die)
            {
                multiplier_speed = 0;
                die = true;
                falling = true;
                managerSound.Instance.Play(splashSFX);
                splashPS.Play();
                rg.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                Collider temp = GetComponent<Collider>();
                temp.enabled = false;
                transform.rotation = Quaternion.Euler(0,180,0);
                transform.parent = null;
            }
        }

        if (shieldActive)
        {
            ShieldPowerUp();
            isShieldActive = true;
        }
        if (!shieldActive)
        {
            StopShield();
            isShieldActive = false;
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

         if (aiming == false &&freezed ==false && multiplier_speed > 0 && check.grounded)
        {
            running = true;
            Vector3 force = new Vector3(x, 0, z);
            Vector3 target_pos = transform.position + force * speed * Time.deltaTime* multiplier_speed;
            target_pos = new Vector3(target_pos.x, transform.position.y, target_pos.z);
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, force, out raycastHit, 4 * speed * Time.deltaTime);
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
        else return;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacles") && check.grounded)rg.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") || check.grounded == false) rg.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
    public IEnumerator SpeedPowerUp()
    {
        if (firstTimeSpeed)
        {
            speed = speed * 1.4f;
            yield return new WaitForSeconds(5f);
            speed = speed / 1.4f;
        }
    }

    public void TeleportPowerUp()
    {
        if (teleportPU)
        {
            if (myBoomerang.shooted && IsGrounded())
            {

                if (playerBoomerang.transform.position.z > this.transform.position.z + 2 || playerBoomerang.transform.position.z < this.transform.position.z - 2 || playerBoomerang.transform.position.x > this.transform.position.x + 5 || playerBoomerang.transform.position.x < this.transform.position.x - 5)
                {
                    if (teleportTimer >= 1)
                    {
                        tpactive = true;
                        transform.position = playerBoomerang.transform.position;
                        myBoomerang.PickUp();
                        Instantiate(teleportPS, transform.position, Quaternion.identity);
                        teleportPS.gameObject.SetActive(true);
                        teleportPS.Play();
                        teleportTimer = 0f;
                    }
                }
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit rhit;
        Physics.Raycast(playerBoomerang.transform.position, Vector3.down, out rhit, 1);
        Color rayColor;
        if (rhit.collider != null && rhit.collider.CompareTag("Ground"))
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(playerBoomerang.transform.position, Vector3.down * 3);
        return rhit.collider != null;
    }



    public void ShieldPowerUp()
    {
        ShieldPS.gameObject.SetActive(true);
        Shield.SetActive(true);
        if (ShieldPS.isEmitting)
        {
            return;
        }
        else
        {
            ShieldPS.Play();
        }
    }
    public void StopShield()
    {
        Shield.SetActive(false);
        ShieldPS.gameObject.SetActive(false);
        ShieldPS.Stop();
    }
    public void Aim(bool x)
    {
        aiming = x;
    }
}

