using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckPlayersReady : MonoBehaviour
{
    [SerializeField] GameObject[] readySprites;
    [SerializeField] Image loadSprite;
    [SerializeField] UnityEvent Ready_Event;

    bool allReady;
    private void Start()
    {
        allReady = false;
        loadSprite.fillAmount = 0;
        for (int i = 0; i < readySprites.Length; i++)
        {
            readySprites[i].SetActive(false);
            
        }
    }
    public void Ready(int indx)
    {
        if (indx <= readySprites.Length - 1)
        {
            if (readySprites[indx].activeSelf)
            {
                readySprites[indx].SetActive(false);
            }
            else
            {
                readySprites[indx].SetActive(true);

            }
        }


        for (int i = 0; i < readySprites.Length; i++)
        {
            if (readySprites[i].activeSelf)
            {
                allReady = true;
            }
            else
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            StartCoroutine(LoadReady());
        }
        else
        {
            BreakReady();
        }
    }

    public void BreakReady()
    {
        StopAllCoroutines();
        loadSprite.fillAmount = 0;

    }

    void LoadGame()
    {
        Ready_Event?.Invoke();
    }

    IEnumerator LoadReady()
    {
        float fill = loadSprite.fillAmount;
        for (float _fill = 0f; _fill < 1.1; _fill += 0.1f)
        {
            fill = _fill;
            loadSprite.fillAmount = fill;

            if (fill >= 1)
            {
                print("!!!ready!!!!");
                LoadGame();
            }
            yield return new WaitForSeconds(.1f);
        }

        //yield return new WaitForSeconds(1.5f);

    }
}
