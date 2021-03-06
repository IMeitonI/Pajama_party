using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Camera : MonoBehaviour
{
    [Header("Jugadores")]
    [SerializeField] private List<Transform> players;
    Transform camera_obj;
    [SerializeField]private bool local_game;
    public static bool local;
    // Start is called before the first frame update
    void Start()
    {
        camera_obj = GetComponent<Transform>();
        var p = GameObject.FindGameObjectsWithTag("Player");
        players = new List<Transform>();
        for (int i = 0; i < p.Length; i++)
        {
            players.Add(p[i].GetComponent<Transform>());
        }
        if (local_game) Mov_Camera.local = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraCalculation();
    }
    private void CameraCalculation()
    {
        if (players == null || players.Count == 0) return;
        Vector3 average_center = Vector3.zero;
        Vector3 total_positions = Vector3.zero;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.activeSelf == false && local_game == false) players.Remove(players[i]);
            else if (players[i].gameObject.activeSelf == false && local_game == true)
            {
                total_positions += new Vector3(0, 0, 0);
            }
            else
            {
                Vector3 player_pos = players[i].position;
                total_positions += new Vector3(player_pos.x, 0f, player_pos.z);
            }
        
        }
        average_center = (total_positions / players.Count);
        camera_obj.position = average_center;
    }
}
