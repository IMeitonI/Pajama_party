using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new skin",menuName ="skin")] 
public class SkinData : ScriptableObject
{
    public int face;
    public int pijama;
    public int boomerang;
   

    public void LoadCharacter(int[] data) {
        if(data.Length>= 3) {
            face = data[0];
            pijama = data[1];
            boomerang = data[2];
          
        }
    }
    public int[] SaveCharacter() {
        int[] data =new int[] {face,pijama,boomerang};

        return data;
    }

    
}
