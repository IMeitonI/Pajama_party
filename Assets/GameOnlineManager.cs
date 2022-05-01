using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameOnlineManager : MonoBehaviourPunCallbacks
{
    public static GameOnlineManager m_Instance;


    [SerializeField] public Player[] playersArray;
    public SkinManager[] playersArrayOBJS;

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
        StartCoroutine(UpdateSkin());
    }

    void Update()
    {

    }

    IEnumerator UpdateSkin()
    {
        playersArrayOBJS = GameObject.FindObjectsOfType<SkinManager>();
        playersArray = PhotonNetwork.PlayerList;
        Debug.Log(playersArray.Length);
        for (int i = 0; i < playersArrayOBJS.Length; i++)
        {
            playersArrayOBJS[i].LoadMesh();
        }
        yield return new WaitForSeconds(1f);
    }

}
