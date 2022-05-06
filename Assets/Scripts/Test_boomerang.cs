using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_boomerang : MonoBehaviour {
    public delegate void BoomerangEvents();
    public event BoomerangEvents Score;
    public event BoomerangEvents DeactiveColider;
    Map_Manager map_Manager;
    public float speed =30, speedRotation;
    float inicialSpeed;
    public Rigidbody rb;
    Vector3 dirVelocity;
    public Transform target, spawn;
    float time, timeBack;
    bool back, reflect = false;
    public bool shooted;
    float distance;
    [SerializeField] AudioClip ReboteSound;
    [Header("VFX")]
    [SerializeField] ParticleSystem collisionPS;

    protected Movement mov;


    private void Awake() {
        map_Manager = FindObjectOfType<Map_Manager>();
        rb = GetComponent<Rigidbody>();
        inicialSpeed = speed;
        gameObject.SetActive(false);
       // map_Manager.Mapchanger += PickUp;

    }

    private void Update() {
       
        if (shooted) { //movimiento y distancia
            distance = Vector3.Distance(target.position, transform.position);
            //if (!reflect && !back) {
            //    transform.eulerAngles = target.transform.localEulerAngles;
            //    print("hotatatata");
            //}
            transform.position += dirVelocity * speed * Time.deltaTime;
            print("Estpy avanzando: ");
        }
        if ((reflect || distance > 15) && !back) { //Reflejo            
            if (speed <= 0 ) {
                Return();
            }           
           speed -= 0.1f;
        }
        if (back) {
            timeBack += Time.deltaTime;
            if (timeBack >= 2) { //Restar velocidad y quedarse quieto             
               speed -= 0.1f;
                if (speed <= 0 ) {
                    speed = 0;
                   
                }
                return;
            }
            if (speed < inicialSpeed) { //Se devuelve
              
                speed += 0.1f;
            }
            if (distance <= 2) {  //Recoge el boomerang
                PickUp();
                return;
            }
            if (reflect) { // tiempo de espera antes de volver a mirar el boomerang
                time += Time.deltaTime;
                if (time >= 0.5f) {
                    reflect = false;
                    time = 0;
                }
                return;
            }
           // LookAtPlayer();
            transform.LookAt(target); // Mirar
            dirVelocity = transform.forward;
        }
    }
    public void Throw() { //Lanzar boomerang
        speed = inicialSpeed;
        dirVelocity = transform.forward ; 
        transform.SetParent(null);
        transform.eulerAngles = spawn.eulerAngles;
        back = false;
        shooted = true;
    }
    void Return() {
        back = true;
        print("Estoy devuelta");
    }
    public void PickUp() { // Recoger boomerang
        timeBack = 0;
        shooted = false;
        back = false;
        reflect = false;
        transform.SetParent(spawn);
        gameObject.transform.eulerAngles = spawn.eulerAngles;
        transform.position = spawn.position;
        gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacles")) {
            PickUp();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == target.gameObject && back) {
            if (back) { PickUp(); }
            else if (Movement.tpactive==true){
                Movement.tpactive = false;
                return;
            }

                  // jugador recoge boomerang
            
        } else if (collision.gameObject.CompareTag("Obstacles")) { //rebote Boomerang
            Return();
            reflect = true;
            print("Estoy rebotando");
            Instantiate(collisionPS, this.transform.position, Quaternion.identity);

            managerSound.Instance.Play(ReboteSound);
            dirVelocity = Vector3.Reflect(dirVelocity, collision.GetContact(0).normal);

        }
        else if(collision.gameObject.CompareTag("Player")) {
                  
            print("Matando a alguien");
            //if (DeactiveColider != null) DeactiveColider();
            if (Score != null) Score();
        }
    }
    //void LookAtPlayer() {
    //    Vector3 direction = target.position - transform.position;
    //    Quaternion toRotation = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speedRotation * Time.deltaTime);
    //}
}
