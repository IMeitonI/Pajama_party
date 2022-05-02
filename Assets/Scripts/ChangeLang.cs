using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ChangeLang : MonoBehaviour
{
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

        //if (Localization_base.instance.language == null)
        //{
        //    Localization_base.instance.language = "SP";
        //}
        //else
        //{
        //    Localization_base.instance.language = Save_Manager.saveM_instance.activeSave.language;

        //}
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
        if (Localization_base.instance.language != null)
        {
            for (int i = 0; i < langsArray.Length; i++)
            {
                if(langsArray[i]== Localization_base.instance.language)
                {
                    curLang = i;
                    break;
                }
            }
            //Localization_base.instance.language = langsArray[curLang];
            langLabel.text = langsText[curLang];
        }

        //if (lang_Event != null)
        //{
        //    lang_Event.Invoke();
        //}

        Localization_base.instance.SetUpTranslation();

        Save_Manager.saveM_instance.activeSave.language = Localization_base.instance.language;
        Save_Manager.saveM_instance.Save();
    }
}
