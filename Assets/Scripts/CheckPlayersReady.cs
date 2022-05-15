using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayersReady : MonoBehaviour
{
    [SerializeField] GameObject[] readySprites;
    public void Ready(int indx)
    {
        if(readySprites[indx].activeSelf)
        {
            readySprites[indx].SetActive(false);
        }
        else
        {
            readySprites[indx].SetActive(true);

        }

        for (int i = 0; i < readySprites.Length; i++)
        {
            if(!readySprites[i].activeSelf)
            {

            }
        }
    }

    public void BreakReady()
    {
        StopAllCoroutines();
    }

    IEnumerator LoadReady()
    {
        yield return new WaitForSeconds(1.5f);

    }
}
