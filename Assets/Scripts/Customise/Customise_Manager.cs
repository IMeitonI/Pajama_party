using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customise_Manager : MonoBehaviour {
    public delegate void SaveTransformation(int a, SkinData skin);
    public event SaveTransformation SaveT;
    [SerializeField]
    GameObject[] buttonsCharacters, buttonsPijamas, buttonsBoomerangs;
    [SerializeField]
    SkinData skin;

    [SerializeField]
    Image faceImage, pijamaImage,boomerangImg;


    [SerializeField]
    Sprite[] faces;
    [SerializeField]
    Sprite[] boomerangs;
    [SerializeField]
    Sprite[] pijamas;

    private void OnEnable()
    {
        Save_Manager.saveM_instance.Load();
        faceImage.sprite = faces[skin.face];
        boomerangImg.sprite = boomerangs[skin.boomerang];
        pijamaImage.sprite = pijamas[skin.pijama];
        UpdateButtons();
    }
    void Start()
    {
        Save_Manager.saveM_instance.Load();
        faceImage.sprite = faces[skin.face];
        boomerangImg.sprite = boomerangs[skin.boomerang];
        pijamaImage.sprite = pijamas[skin.pijama];
        UpdateButtons();
    }

    void UpdateButtons()
    {
        for (int i = 0; i < buttonsCharacters.Length; i++)//face
        {
            buttonsCharacters[i].SetActive(false);
        }
        for (int i = 0; i < buttonsPijamas.Length; i++)//pijama
        {
            buttonsPijamas[i].SetActive(false);
        }
        for (int i = 0; i < buttonsBoomerangs.Length; i++)//boomerang
        {
            buttonsBoomerangs[i].SetActive(false);
        }
        buttonsCharacters[skin.face].SetActive(true);
        buttonsPijamas[skin.pijama].SetActive(true);
        buttonsBoomerangs[skin.boomerang].SetActive(true);
    }

    public void selectFace(int face)
    {
        skin.face = face;
        faceImage.sprite = faces[skin.face];

        Save_Manager.saveM_instance.Save();
        UpdateButtons();
    }

    public void selectPijama(int pijama)
    {
        skin.pijama = pijama;
        pijamaImage.sprite = pijamas[skin.pijama];
        Save_Manager.saveM_instance.Save();

        UpdateButtons();
    }

    public void selectBoomerang(int boomerang)
    {
        skin.boomerang = boomerang;
        boomerangImg.sprite = boomerangs[skin.boomerang];
        Save_Manager.saveM_instance.Save();

        UpdateButtons();
    }

}
