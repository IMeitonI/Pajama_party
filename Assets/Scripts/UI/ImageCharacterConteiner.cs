using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class ImageCharacterConteiner :MonoBehaviour
{
    Sprite sprite;
    public static ImageCharacterConteiner instance;
    [Tooltip("mapache:0, leon:1, zorro:2, perro:3")][SerializeField] public 
    Sprite [] characters =new Sprite[4];

    private void Awake() {
        instance = this;
    }
    public Sprite GetSprite(int player) {
        sprite = characters[player];
        return sprite;
    }

}
