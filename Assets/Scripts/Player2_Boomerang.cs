using UnityEngine;
using UnityEngine.UI;

public class Player2_Boomerang : MonoBehaviour
{
    Map_Manager map_Manager;
    [SerializeField] public BoomerangLogic myBoomerang;
    protected int score;
    public bool alive;
    public bool first_hit = false;
    Text myText;
    protected CapsuleCollider myCollider;
    protected Movement mov;
    protected Rigidbody rb;

    [Header("Sounds")]
    [SerializeField] protected AudioClip DieSound;
    [SerializeField] protected AudioClip ShootSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // myBoomerang.DeactiveColider += DeactivateCol;
        map_Manager = FindObjectOfType<Map_Manager>();
        myCollider = GetComponent<CapsuleCollider>();
        alive = true;
        // myBoomerang.target = transform;
        mov = GetComponent<Movement>();

    }
    private void OnEnable()
    {
        Map_Manager.Mapchanger -= Activatecollider;
    }
    private void OnDisable()
    {
        Map_Manager.Mapchanger += Activatecollider;
    }

    // public void Shoot() {
    //     if (myBoomerang.shooted || !gameObject.activeSelf || mov.die == true) return;
    //     myBoomerang.gameObject.SetActive(true);

    //     myBoomerang.Throw();
    //     //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
    //     managerSound.Instance.Play(ShootSound);

    // }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject != myBoomerang.colEfector && other.gameObject.CompareTag("Boomerang"))
        {
            BoomerangLogic colBoomerang = other.gameObject.GetComponentInParent<BoomerangLogic>();
            if (mov.shieldActive && colBoomerang.boomerangVelocity > 3 && first_hit == false)
            {
                // myBoomerang.ReturnBoomerang();
                first_hit = true;
                mov.shieldActive = false;
                return;
            }
            else
            {
                if (colBoomerang.boomerangVelocity < 3) return;

                if (alive == true && first_hit == false)
                {
                    DeactivateCol();
                    alive = false;
                    first_hit = true;
                    colBoomerang.KillSomeOne();
                    myBoomerang.ReturnBoomerang();
                    AnimatorController anim = GetComponent<AnimatorController>();
                    if (anim == null)
                    {
                        AnimatorControllerOnline animOn = GetComponentInChildren<AnimatorControllerOnline>();
                        animOn.Die();
                    }
                    else anim.Die();

                    // Modificaci n Jose
                    //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
                    managerSound.Instance.Play(DieSound);
                    //Hasta ac  

                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != myBoomerang.colEfector && other.gameObject.CompareTag("Boomerang")) first_hit = false;
    }
    protected void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject != myBoomerang.gameObject && other.gameObject.CompareTag("Boomerang"))
        // {
        //     // if (mov.shieldActive)
        //     // {
        //     //     myBoomerang.Return();
        //     //     mov.shieldActive = false;
        //     //     return;
        //     // }
        //     else
        //     {
        //         if (other.gameObject.GetComponent<Test_boomerang>().speed == 0) return;
        //         if (alive == true)
        //         {
        //             DeactivateCol();

        //             AnimatorController anim = GetComponent<AnimatorController>();
        //             if (anim == null)
        //             {
        //                 AnimatorControllerOnline animOn = GetComponentInChildren<AnimatorControllerOnline>();
        //                 animOn.Die();
        //             }
        //             else anim.Die();
        //             alive = false;
        //             // Modificaci n Jose
        //             //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
        //             managerSound.Instance.Play(DieSound);
        //             //Hasta ac  

        //         }
        //     }

        // }
    }

    protected void Activatecollider()
    {
        //rb.useGravity = true;
        myCollider.enabled = true;
        rb.isKinematic = false;
        alive = true;
        Movement.multiplier_speed = 1;
    }
    protected void DeactivateCol()
    {
        //rb.useGravity = false;
        rb.isKinematic = true;
        myCollider.enabled = false;
        Movement.multiplier_speed = 0;
        mov.firstTimeFalling = true;
        mov.falling = false;
        mov.die = false;
    }
}
