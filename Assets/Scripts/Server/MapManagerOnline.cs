using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagerOnline : MonoBehaviour
{
    [SerializeField] public GameObject[] maps;
    GameObject[] players;
    public static bool winner;
    public static bool changing_mp;
    [Range(0, 1)]
    int counter;
    public int current_map;
    public static int players_deaths;
    [SerializeField] GameObject scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        FillPlayers();
        current_map = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length == 0 || players == null) FillPlayers();
        if (players_deaths >= players.Length) ActiveScoreBoard();
    }
    public void FillPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Teleport_players(current_map);
    }
    public void ChangeMap()
    {
        changing_mp = true;
        current_map++;
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }
        if (current_map >= maps.Length) current_map = 0;
        maps[current_map].SetActive(true);
        players_deaths = 0;
        Teleport_players(current_map);
    }
    private void Teleport_players(int map)
    {
        int spawns = maps[map].transform.GetChild(0).transform.childCount;
        Transform[] spawnpoints = new Transform[spawns];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = maps[map].transform.GetChild(0).GetChild(i).transform;
        }
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnpoints[i].position;
        }
        Invoke("DisableScoreBoard", 3f);
    }
    public void ActiveScoreBoard()
    {
        scoreBoard.SetActive(true);
        if (counter == 0)
        {
            counter = 1;
            ChangeMap();
        }
    }
    public void DisableScoreBoard()
    {
        counter = 0;
        scoreBoard.SetActive(false);
        changing_mp = false;
    }
}
