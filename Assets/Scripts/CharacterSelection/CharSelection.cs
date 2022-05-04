using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelection : MonoBehaviour
{
    public PlayerModels[] playerModels;
    public Transform spot;

    private List<GameObject> players;
    public int currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();

        foreach (var playerModel in playerModels)
        {
            GameObject go = Instantiate(playerModel.playerObject, spot.position, Quaternion.identity);
            go.SetActive(false);
            go.transform.SetParent(spot);
            players.Add(go);
        }

        ShowPlayerFromList();
    }

    void ShowPlayerFromList()
    {
        players[currentPlayer].SetActive(true);
    }

    public void OnClickNext()
    {
        players[currentPlayer].SetActive(false);
        if(currentPlayer < players.Count - 1)
        {
            currentPlayer = currentPlayer + 1;
        }
        else
        {
            currentPlayer = 0;
        }
        ShowPlayerFromList();
    }

    public void OnClickPrev()
    {
        players[currentPlayer].SetActive(false);
        if (currentPlayer == 0)
        {
            currentPlayer = players.Count - 1;
        }
        else
        {
            currentPlayer = currentPlayer -1;
        }
        ShowPlayerFromList();
    }

    //public void SavePlayer(bool firstPlayer) { //REVISAR    
    //    if (firstPlayer) {
    //        Save_Manager.saveManagerInstance.activeSave.character_1 = playerModels[currentPlayer].playerObject;
    //    } else { Save_Manager.saveManagerInstance.activeSave.character_2 = playerModels[currentPlayer].playerObject; }

    //    Save_Manager.saveManagerInstance.Save();
    //}

}
