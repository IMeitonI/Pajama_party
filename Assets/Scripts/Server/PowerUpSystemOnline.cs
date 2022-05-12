using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystemOnline : MonoBehaviour, IOnEventCallback
{
    private const byte CureEventCode = 1;
    [SerializeField] GameObject prefab;
    float time;
    float cooldown = 10;
    int current_pos, counter;
    Transform[] spawns;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        counter = 1;
    }

    private void GenerateCure()
    {

        RaiseEventOptions eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All, CachingOption = EventCaching.AddToRoomCache };

        PhotonNetwork.RaiseEvent(CureEventCode, null, eventOptions, SendOptions.SendReliable);

    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= cooldown && counter == 1)
        {
            counter = 0;
            GenerateCure();
        }
    }


    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CureEventCode)
        {
            time = 0;
            spawns = GameObject.Find("Spawnpoints_PowerUps").GetComponentsInChildren<Transform>();
            Instantiate(prefab, spawns[current_pos].position, Quaternion.identity);
            current_pos++;
            if (current_pos >= spawns.Length - 1) current_pos = 0;
            counter = 1;
        }
    }

}
