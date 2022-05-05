using UnityEngine;
using UnityEngine.UI;

public class Player2_Boomerang : MonoBehaviour {
    Map_Manager map_Manager;
    [SerializeField] public Test_boomerang myBoomerang;
    int score;
    bool alive;
    Text myText;
    protected CapsuleCollider myCollider;
    protected Movement mov;
    protected Rigidbody rb;

    [Header("Sounds")]
    [SerializeField] AudioClip DieSound;
    [SerializeField] AudioClip ShootSound;

    void Start() {
        rb = GetComponent<Rigidbody>();
        myBoomerang.DeactiveColider += DeactivateCol;
        map_Manager = FindObjectOfType<Map_Manager>();
        myCollider = GetComponent<CapsuleCollider>();
        alive = true;
        myBoomerang.target = transform;
        mov = GetComponent<Movement>();
        map_Manager.Mapchanger += Activatecollider;
    }

    public void Shoot() {
       
        if (myBoomerang.shooted || !gameObject.activeSelf) return;
        myBoomerang.gameObject.SetActive(true);
        
        myBoomerang.Throw();
        //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
        managerSound.Instance.Play(ShootSound);

    }
    protected void OnCollisionEnter(Collision other) {
        if (other.gameObject != myBoomerang.gameObject && other.gameObject.CompareTag("Boomerang")) {
            if (mov.shieldActive)
            {
                return;
            }
            else
            { if (other.gameObject.GetComponent<Test_boomerang>().speed == 0) return;
                 
                  DeactivateCol();
                
                AnimatorController anim = GetComponent<AnimatorController>();
                if (anim == null)
                {
                    AnimatorControllerOnline animOn = GetComponentInChildren<AnimatorControllerOnline>();
                    animOn.Die();
                }
                else anim.Die();
                alive = false;
                // Modificaci n Jose
                //managerSound manager = GameObject.Find("MainSound").GetComponent<managerSound>();
                managerSound.Instance.Play(DieSound);
                //Hasta ac  

            }

        }
    }

    protected void Activatecollider() {
        //rb.useGravity = true;
        myCollider.enabled = true;
        rb.isKinematic = false;

    }
    protected void DeactivateCol() {
        //rb.useGravity = false;
        rb.isKinematic = true;
        myCollider.enabled = false;
    }
}
