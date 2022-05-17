using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ScoreBoardOnline : MonoBehaviour
{
    [SerializeField] Transform conteiner;
    [SerializeField] GameObject scoreBoardPrefab;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreBoard(player);
        }
    }
    void AddScoreBoard(Player player)
    {
        LoadUIScoreImage item= Instantiate(scoreBoardPrefab, conteiner).GetComponent<LoadUIScoreImage>();
        item.InstanceOnlineImage(player);
    }
}
