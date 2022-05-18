using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] float grabDis = 10f;
    [SerializeField] float recallSpeed = 80f;
    [SerializeField] float lookSpeed = 80f;
    [SerializeField] float timeToReturn = 2f;
    [SerializeField] int countCollisions;
    [SerializeField] Vector3 boomerangVelocityVector;
    [SerializeField] float boomerangVelocity;

    [SerializeField] GameObject boomerangAnim;
    public bool canReturn;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        state = State.WithPlayer;
        countCollisions = 0;
        canReturn = false;
        playerLauncherRef.isReturning = false;
        playerLauncherRef.ButtonMagnet.SetActive(false);
        this.gameObject.transform.SetParent(null);
        // Time.timeScale=0.2f;
    }

    public void GiveBoomerang()
    {
        state = State.Thrown;

    }

    private void Update()
    {
        boomerangAnim.transform.eulerAngles = new Vector3(boomerangAnim.transform.eulerAngles.x, boomerangAnim.transform.eulerAngles.y + boomerangVelocity, boomerangAnim.transform.eulerAngles.z);
        Debug.Log("state: " + state);
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
                LookAtPlayer();
                // Vector3 dirToPlayer = (playerLauncherRef.GetPosition() - transform.position).normalized;
                Vector3 dirToPlayer = transform.forward;
                rb.velocity = dirToPlayer * recallSpeed;

                if (Vector3.Distance(transform.position, GetPlayerPos()) < grabDis)
                {
                    state = State.WithPlayer;
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    countCollisions = 0;
                    canReturn = false;
                    playerLauncherRef.isReturning = false;
                    playerLauncherRef.ButtonMagnet.SetActive(false);
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
            state = State.WithPlayer;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            countCollisions = 0;
            canReturn = false;
            playerLauncherRef.isReturning = false;
            playerLauncherRef.ButtonMagnet.SetActive(false);
        }
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
    }

    Vector3 GetPlayerPos()
    {
        return new Vector3(playerLauncherRef.transform.position.x, this.transform.position.y, playerLauncherRef.transform.position.z);
    }
    public void ThrowBoomerang(Vector3 throwDir, float throwForce)
    {
        rb.velocity = Vector3.zero;
        transform.position = GetPlayerPos() + throwDir * (grabDis + 0.2f);
        rb.isKinematic = false;
        rb.AddForce(throwDir * throwForce, ForceMode.Impulse);
        state = State.Thrown;
        // playerLauncherRef.isReturning = false;
        canReturn = true;
        StartCoroutine(ReCallingCount());
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
            // Debug.Log(countCollisions);
            // if (countCollisions == 3)
            // {
            //     ReCall();
            // }
            if (countCollisions >= 1 && state == State.Recalling)
            {
                state = State.Thrown;
            }
        }
    }
}
