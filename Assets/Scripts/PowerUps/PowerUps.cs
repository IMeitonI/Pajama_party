using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    [Range(0, 2)]
    [Tooltip("Speed = 0, Shield = 1, Teleport = 2")]
    [SerializeField] int powerUpID;
    private Movement _player;

    [SerializeField]
    private ParticleSystem teleportPickupVFX;

    [SerializeField]
    private ParticleSystem speedPickupVFX;

    [SerializeField]
    private ParticleSystem shieldPickupVFX;

    public int index = 0;

    private bool speedBool = true;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        powerUpID = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<Movement>();
            if (_player != null)
            {
                PowerUpsManager.PickUpPowerUp();
                switch (powerUpID)
                {
                    case 0:
                        if (speedBool)
                        {
                            StartCoroutine(_player.SpeedPowerUp());
                            Instantiate(speedPickupVFX);
                            _player.firstTimeSpeed = false;
                            speedBool = false;
                            index += 1;
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 1:
                        _player.shieldActive = true;
                        Instantiate(shieldPickupVFX);
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        _player.teleportPU = true;
                        Instantiate(teleportPickupVFX);
                        Destroy(this.gameObject);
                        //_player.TeleportButton.gameObject.SetActive(true);
                        //_player.TeleportPowerUp();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5f);
        _player.StopShield();
    }
}
