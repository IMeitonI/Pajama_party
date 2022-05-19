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
    static float time;
    float cooldown = 10;
    int current_pos, counter;
    Transform[] spawns;
    MapManagerOnline map_manager;
    int current_map;
    public static int pw_spawned;

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
        map_manager = GetComponent<MapManagerOnline>();
        current_map = 99;
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
            if(pw_spawned<2)GenerateCure();
        }
    }


    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CureEventCode)
        {
            time = 0;
            pw_spawned++;
            if (current_map != map_manager.current_map)
            {
                int length_spawns = map_manager.maps[map_manager.current_map].transform.GetChild(1).transform.childCount;
                spawns = new Transform[length_spawns];
                for (int i = 0; i < spawns.Length; i++)
                {
                    spawns[i] = map_manager.maps[map_manager.current_map].transform.GetChild(1).GetChild(i).transform;
                }
                current_map = map_manager.current_map;
            }         
            Instantiate(prefab, spawns[current_pos].position, Quaternion.identity);
            current_pos++;
            if (current_pos >= spawns.Length - 1) current_pos = 0;
            counter = 1;
        }
    }
    public static void PickUpPowerUp()
    {
        if (pw_spawned >= 2)
        {
            pw_spawned--;
            time = 0;
        }
        else pw_spawned--;
    }
}
