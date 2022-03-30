using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public delegate void PickupE();
    public event PickupE Mapchanger;
    [SerializeField] public GameObject[] maps;
    GameObject[] players;
    public static bool winner;
    public static bool change_mp;
    [Range(0, 1)]
    int counter;
    public static int players_deaths;
    [SerializeField] private GameObject score_panel;

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
        if (Mov_Camera.local == false)
        {
            if (players_deaths == players.Length - 1 && counter == 0)
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
            score_panel?.SetActive(false);
        }
    }

    private void ChangeMap()
    {
        if (Mapchanger != null) Mapchanger();
        score_panel?.SetActive(true);
        int rnd = Random.Range(0, maps.Length - 1);
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }
        maps[rnd].SetActive(true);
        Teleport_players(rnd);
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
            if (Mov_Camera.local == false)
            {
                players[i].gameObject.SetActive(true);
                Debug.Log(players[i].activeSelf);
                if (players[i].activeSelf == false) players[i].gameObject.SetActive(true);
            }
            if (Mov_Camera.local == false) Invoke("DisableCanvas", 5f);
        }
    }
}
