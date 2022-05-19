using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public delegate void PickupE();
    static public event PickupE Mapchanger;
    [SerializeField] public GameObject[] maps;
    GameObject[] players;
    public static bool winner;
    public static bool change_mp;
    [Range(0, 1)]
    int counter; 
    public int current_map;
    public static int players_deaths;
    [SerializeField] private GameObject score_panel;
    [SerializeField] CambioMapaBarrera barrera; //Majo
    private void Start()
    {
        //Instantiate(Save_Manager.saveManager.activeSave.character_1);
        //Instantiate(Save_Manager.saveManager.activeSave.character_2);

        ////players[1] =Save_Manager.saveManager.activeSave.character_1;
        ////players[2] = Save_Manager.saveManager.activeSave.character_2;
        players = GameObject.FindGameObjectsWithTag("Player");
        change_mp = false;
        winner = false;
        if (Mov_Camera.local)
        {
            if (players[0].gameObject.name == "P1") return;
            else
            {
                GameObject temp = players[0];
                players[0] = players[1];
                players[1] = temp;
            }
        }
        else Teleport_players(current_map);
    }
    private void FixedUpdate()
    {
        if (Mov_Camera.local)
        {
            if (players[0].gameObject.name != "P1")
            {
                GameObject temp = players[0];
                players[0] = players[1];
                players[1] = temp;
            }
        }
        if (Mov_Camera.local == false )
        {
            if (players_deaths == players.Length -1 && counter == 0)
            {
                counter = 1;
                ChangeMap();

            }
        }
        else if (change_mp && counter == 1)
        {
            counter = 0;
            ChangeMap();
        }
        else if (change_mp == false)
        {
            counter = 1;
            DisableCanvas();
        }
        else return;

    }

    private void DisableCanvas()
    {
        if (winner == false)
        {
            if (Mov_Camera.local == false)
            {
                players_deaths = 0;
                counter = 0;
            }
            else
            {
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].gameObject.SetActive(true);
                }
            }
     
            score_panel?.SetActive(false);
        }
    }

    private void ChangeMap()
    {
        // if (Mapchanger != null) Mapchanger();
        Mapchanger?.Invoke();
        score_panel?.SetActive(true);
        current_map++;
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }
        if (current_map >= maps.Length) current_map = 0;
        maps[current_map].SetActive(true);
        barrera.CambiarUbicacionBarrera();
        Teleport_players(current_map);
    }
    private void Teleport_players(int rnd)
    {

        int spawns = maps[rnd].transform.GetChild(0).transform.childCount;
        Transform[] spawnpoints = new Transform[spawns];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = maps[rnd].transform.GetChild(0).GetChild(i).transform;
        }
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnpoints[i].position;
            if (Mov_Camera.local == false )
            {
                players[i].gameObject.SetActive(true);
                Debug.Log(players[i].activeSelf);
                if (players[i].activeSelf == false ) players[i].gameObject.SetActive(true);
            }
            if (Mov_Camera.local == false ) Invoke("DisableCanvas", 5f);
        }
    }
}
