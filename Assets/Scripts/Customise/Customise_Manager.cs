using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customise_Manager : MonoBehaviour {
    public delegate void SaveTransformation(int a, Online_skin skin);
    public event SaveTransformation SaveT;
    [SerializeField]
    GameObject[] buttonsCharacters, buttonsPijamas, buttonsBoomerangs;
    [SerializeField]
    Online_skin skin;

    [SerializeField]
    Image faceImage, pijamaImage,boomerangImg;


    [SerializeField]
    Sprite[] faces;
    [SerializeField]
    Sprite[] boomerangs;
    [SerializeField]
    Color[] pijamas;


    void Start()
    {
        faceImage.sprite = faces[skin.face];
        boomerangImg.sprite = boomerangs[skin.boomerang];
        pijamaImage.color = pijamas[skin.pijama];
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
        if (SaveT != null) SaveT(skin.player, skin);
       
       
        UpdateButtons();
    }

    public void selectPijama(int pijama)
    {
        skin.pijama = pijama;
        pijamaImage.color = pijamas[skin.pijama];
        if (SaveT != null) SaveT(skin.player, skin);
        UpdateButtons();
    }

    public void selectBoomerang(int boomerang)
    {
        skin.boomerang = boomerang;
        boomerangImg.sprite = boomerangs[skin.boomerang];
        if (SaveT != null) SaveT(skin.player, skin);
        UpdateButtons();
    }

}
