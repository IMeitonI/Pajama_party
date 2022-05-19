using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    Map_Manager map_manager;
    [SerializeField] GameObject power_up;
    [SerializeField] private float spawn_time;
    private static float time;
    private int rnd;
    public static int pw_spawned;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        map_manager = GetComponent<Map_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Time: "+time);
        Debug.Log(pw_spawned);
        time += Time.deltaTime;
        if (time >= spawn_time)
        {
            time = 0;
            if(pw_spawned<2)SpawnPowerUp();
        }
        if (Map_Manager.change_mp)
        {
            time = 0;
            pw_spawned = 0;
        }
    }

    private void SpawnPowerUp()
    {
        pw_spawned++;
        int spawns = map_manager.maps[map_manager.current_map].transform.GetChild(1).transform.childCount;
        Transform[] spawnpoints = new Transform[spawns];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = map_manager.maps[map_manager.current_map].transform.GetChild(1).GetChild(i).transform;
        }
        int past_rnd = rnd;
        rnd =  Random.Range(0, spawns);
        while (past_rnd == rnd) rnd = Random.Range(0, spawns);
        Instantiate(power_up, spawnpoints[rnd]);

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
