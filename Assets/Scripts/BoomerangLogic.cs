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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        state = State.Recalling;
        countCollisions = 0;
        // Time.timeScale=0.1f;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Recalling:
                LookAtPlayer();
                // Vector3 dirToPlayer = (playerLauncherRef.GetPosition() - transform.position).normalized;
                Vector3 dirToPlayer = transform.forward;
                rb.velocity = dirToPlayer * recallSpeed;

                if (Vector3.Distance(transform.position, playerLauncherRef.GetPosition()) < grabDis)
                {
                    state = State.WithPlayer;
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    countCollisions = 0;

                }

                break;
        }
    }

    IEnumerator ReCallingCount()
    {
        yield return new WaitForSeconds(timeToReturn);
        if (state == State.Thrown)
        {
            ReCall();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = playerLauncherRef.GetPosition() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lookSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case State.WithPlayer:
                transform.position = playerLauncherRef.GetPosition();
                break;
        }
    }
    public void ThrowBoomerang(Vector3 throwDir, float throwForce)
    {
        rb.velocity = Vector3.zero;
        transform.position = playerLauncherRef.gameObject.transform.position;
        rb.isKinematic = false;
        rb.AddForce(throwDir * throwForce, ForceMode.Impulse);
        state = State.Thrown;
        // StartCoroutine(ReCallingCount());
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
        countCollisions += 1;
        Debug.Log(countCollisions);
        if (countCollisions >= 4)
        {
            ReCall();
        }
    }
}
