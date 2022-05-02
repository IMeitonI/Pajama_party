using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ChangeLang : MonoBehaviour
{
    Localization_base langData;
    [SerializeField] TMP_Text langLabel;

    [SerializeField] UnityEvent lang_Event;
    int curLang;
    string[] langsArray = new string[]
    {
        "SP",
        "ENG",

    };

    string[] langsText = new string[]
    {
        "Spanish",
        "English",
    };

    void Start()
    {
        langData = GameObject.FindGameObjectWithTag("Manager").GetComponent<Localization_base>();

        if (langData.language == null)
        {
            langData.language = "SP";
        }
        else
        {
            langData.language = Save_Manager.saveM_instance.activeSave.language;

        }
        SetLang();
    }

    public void LeftButton()
    {
        if (curLang > 0)
        {
            curLang -= 1;
        }
        else
        {
            curLang = langsArray.Length - 1;
        }

        print("lang is: " + curLang);
        SetLang();

    }

    public void RightButton()
    {
        

        if (curLang < langsArray.Length-1)
        {
            curLang += 1;
        }
        else
        {
            curLang = 0;
        }
        Debug.Log("lang is: " + curLang);
        SetLang();
    }


    public void SetLang()
    {
        if (langData != null)
        {
            langData.language = langsArray[curLang];
            langLabel.text = langsText[curLang];
        }

        if (lang_Event != null)
        {
            lang_Event.Invoke();
        }
        Save_Manager.saveM_instance.activeSave.language = langData.language;
        Save_Manager.saveM_instance.Save();
    }
}
