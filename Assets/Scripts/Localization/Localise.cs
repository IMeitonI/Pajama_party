using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localise : MonoBehaviour
{
    [SerializeField] string key;
    TMP_Text text_localized;

    private void Awake()
    {
        text_localized = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        //Localization_base.instance.TranslateLoad += SetLang;
        //if (text_localized != null)
        SetLang();
    }

    private void OnDisable()
    {
        //Localization_base.instance.TranslateLoad -= SetLang;
    }
    void Start()
    {
        SetLang();
    }

    public void SetLang()
    {
        if (Localization_base.instance != null && !string.IsNullOrEmpty(key))
        {
            string translate = Localization_base.instance.GetTraslation(key);
            if (key != null && translate != null)
            {
                if (text_localized != null)
                    text_localized.text = translate;
            }
        }
    }

}
