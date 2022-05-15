using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpLocalise : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    void Start()
    {
        settingsMenu.SetActive(true);
        StartCoroutine(OffSettings());
    }

    IEnumerator OffSettings()
    {
        yield return new WaitForSeconds(0.1f);
        settingsMenu.SetActive(false);
    }
}
