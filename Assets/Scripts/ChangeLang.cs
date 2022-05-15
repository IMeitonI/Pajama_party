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
        //Localization_base.instance.language = Save_Manager.saveM_instance.activeSave.language;

        //if (Localization_base.instance.language == null)
        //{
        //    Localization_base.instance.language = "SP";
        //}
        //else
        //{
        //    Localization_base.instance.language = Save_Manager.saveM_instance.activeSave.language;

        //}
        for (int i = 0; i < langsArray.Length; i++)
        {
            if (langsArray[i] == Save_Manager.saveM_instance.activeSave.language)
            {
                curLang = i;
                Localization_base.instance.language = langsArray[curLang];
                langLabel.text = langsText[curLang];
                break;
            }
        }

        SetLang();

    }
    private void OnEnable()
    {

        Localization_base.TranslateLoad += SetLang;

    }

    private void OnDisable()
    {
        Localization_base.TranslateLoad -= SetLang;

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
        Save_Manager.saveM_instance.activeSave.language = Localization_base.instance.language;
        Save_Manager.saveM_instance.Save();
    }

    public void RightButton()
    {

        if (curLang < langsArray.Length - 1)
        {
            curLang += 1;
        }
        else
        {
            curLang = 0;
        }
        Debug.Log("lang is: " + curLang);
        SetLang();
        Save_Manager.saveM_instance.activeSave.language = Localization_base.instance.language;
        Save_Manager.saveM_instance.Save();
    }


    public void SetLang()
    {
        Localization_base.instance.language = Save_Manager.saveM_instance.activeSave.language;

        if (Localization_base.instance.language != null)
        {

            Localization_base.instance.language = langsArray[curLang];
            langLabel.text = langsText[curLang];

        }

        lang_Event?.Invoke();

        //Localization_base.instance.SetUpTranslation();

        
    }
}
