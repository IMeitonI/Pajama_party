using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localise : MonoBehaviour
{
    [SerializeField] string key;
    TMP_Text text_localized;
    // Start is called before the first frame update
    private void Awake()
    {
        text_localized = GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        Localization_base.instance.TranslateLoad -= SetLang;

    }
    void Start()
    {
        Localization_base.instance.TranslateLoad += SetLang;
        
        if (Localization_base.instance!=null&&!string.IsNullOrEmpty(key))
        {
            string translate = Localization_base.instance.GetTraslation(key);
            if (key != null&& translate!=null)
            {
                text_localized.text = translate;
            }
        }
        
    }

    public void SetLang()
    {
        if (Localization_base.instance != null && !string.IsNullOrEmpty(key))
        {
            string translate = Localization_base.instance.GetTraslation(key);
            if (key != null && translate != null)
            {
                text_localized.text = translate;
            }
        }
    }

}
