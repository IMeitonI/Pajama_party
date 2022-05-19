using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangLogic : MonoBehaviour
{

    private enum State
    {
        WithPlayer,
        Thrown,
        Recalling,
    }

    private State state;
    private Rigidbody rb;
    [SerializeField] private BoomerangLauncher playerLauncherRef;
    public GameObject colEfector;

    private SphereCollider colliderBoomerang;
    [SerializeField] float grabDis = 10f;
    [SerializeField] float recallSpeed = 80f;
    [SerializeField] float lookSpeed = 80f;
    [SerializeField] float timeToReturn = 2f;
    [SerializeField] int countCollisions;
    [SerializeField] Vector3 boomerangVelocityVector;
    [SerializeField] public float boomerangVelocity;

    [SerializeField] GameObject boomerangAnim;
    [SerializeField] GameObject trail;
    public bool canReturn;
    [SerializeField] public UnityEvent killEvent;

    [SerializeField] AudioClip boomerangHit;

    void Awake()
    {
        boomerangAnim.SetActive(false);
        colliderBoomerang = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        state = State.WithPlayer;
        countCollisions = 0;
        canReturn = false;
        playerLauncherRef.isReturning = false;
        this.gameObject.transform.SetParent(playerLauncherRef.transform);
        rb.isKinematic = true;
        playerLauncherRef.ButtonMagnet.SetActive(false);
        trail.SetActive(false);
        colliderBoomerang.enabled = false;
        // Time.timeScale=0.2f;
    }

    public void KillSomeOne()
    {
        killEvent?.Invoke();
    }

    public void GiveBoomerang()
    {
        state = State.Thrown;

    }

    private void Update()
    {

        boomerangAnim.transform.eulerAngles = new Vector3(boomerangAnim.transform.eulerAngles.x, boomerangAnim.transform.eulerAngles.y + boomerangVelocity, boomerangAnim.transform.eulerAngles.z);

    }

    private void OnEnable()
    {
        Map_Manager.Mapchanger += ReturnBoomerang;
    }
    private void OnDisable()
    {
        Map_Manager.Mapchanger -= ReturnBoomerang;

    }
    private void FixedUpdate()
    {
        boomerangVelocityVector = rb.velocity;
        boomerangVelocity = boomerangVelocityVector.magnitude;




        switch (state)
        {
            case State.Thrown:
                TryGrabBoomerang();
                break;
            case State.Recalling:
                trail.SetActive(true);
                LookAtPlayer();
                // Vector3 dirToPlayer = (playerLauncherRef.GetPosition() - transform.position).normalized;
                Vector3 dirToPlayer = transform.forward;
                rb.velocity = dirToPlayer * recallSpeed;

                if (Vector3.Distance(transform.position, GetPlayerPos()) < grabDis)
                {
                    boomerangAnim.SetActive(false);
                    state = State.WithPlayer;
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    countCollisions = 0;
                    canReturn = false;
                    playerLauncherRef.isReturning = false;
                    this.gameObject.transform.SetParent(playerLauncherRef.transform);
                    playerLauncherRef.ButtonMagnet.SetActive(false);
                    trail.SetActive(false);
                    colliderBoomerang.enabled = false;

                }

                break;
        }
    }

    IEnumerator ReCallingCount()
    {
        yield return new WaitForSeconds(timeToReturn);
        if (state == State.Thrown && countCollisions < 1)
        {
            // recallSpeed = boomerangVelocity;
            playerLauncherRef.isReturning = true;
            ReCall();
        }
    }

    private void TryGrabBoomerang()
    {
        if (Vector3.Distance(transform.position, GetPlayerPos()) < grabDis)
        {
            boomerangAnim.SetActive(false);
            state = State.WithPlayer;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            countCollisions = 0;
            canReturn = false;
            playerLauncherRef.isReturning = false;
            this.gameObject.transform.SetParent(playerLauncherRef.transform);
            playerLauncherRef.ButtonMagnet.SetActive(false);
            trail.SetActive(false);
            colliderBoomerang.enabled = false;
        }
    }

    public void ReturnBoomerang()
    {
        boomerangAnim.SetActive(false);
        state = State.WithPlayer;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        countCollisions = 0;
        canReturn = false;
        playerLauncherRef.isReturning = false;
        this.gameObject.transform.SetParent(playerLauncherRef.transform);
        playerLauncherRef.ButtonMagnet.SetActive(false);
        trail.SetActive(false);
        colliderBoomerang.enabled = false;

    }



    void LookAtPlayer()
    {
        Vector3 direction = GetPlayerPos() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lookSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {

        // if(boomerangVelocity<=0&&state==State.Thrown)ReCall();

        switch (state)
        {
            case State.WithPlayer:
                transform.position = GetPlayerPos();
                break;
        }

        if (boomerangVelocity < 3 && state == State.Thrown)
        {
            trail.SetActive(false);
        }
        else if (boomerangVelocity > 3 && state == State.Thrown)
        {
            trail.SetActive(true);
        }

    }

    Vector3 GetPlayerPos()
    {
        return new Vector3(playerLauncherRef.transform.position.x, playerLauncherRef.transform.position.y, playerLauncherRef.transform.position.z);
    }
    public void ThrowBoomerang(Vector3 throwDir, float throwForce)
    {
        boomerangAnim.SetActive(true);
        rb.velocity = Vector3.zero;
        transform.position = GetPlayerPos() + throwDir * (grabDis + 0.5f);
        rb.isKinematic = false;
        this.gameObject.transform.SetParent(null);
        rb.AddForce(throwDir * throwForce, ForceMode.Impulse);
        state = State.Thrown;
        // playerLauncherRef.isReturning = false;
        canReturn = true;
        StartCoroutine(ReCallingCount());
        trail.SetActive(true);
        colliderBoomerang.enabled = true;
    }

    public void ReCall()
    {
        state = State.Recalling;
    }

    public void Nothing()
    {
        state = State.Thrown;

    }

    public bool IsWithPlayer()
    {
        return state == State.WithPlayer;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "BoomerangGround")
        {

            countCollisions += 1;
            managerSound.Instance.Play(boomerangHit);

            if (countCollisions >= 1 && state == State.Recalling)
            {
                playerLauncherRef.isReturning = false;
                state = State.Thrown;
                // Debug.Log("countercols: " + countCollisions + "state: " + state);
            }
        }
    }
}
