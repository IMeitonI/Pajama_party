using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameOnlineManager : MonoBehaviourPunCallbacks
{
    public static GameOnlineManager m_Instance;


    [SerializeField] public Player[] playersArray;

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
    }

    void Update()
    {

    }

}
