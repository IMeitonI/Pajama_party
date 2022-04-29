using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new skin",menuName ="skin")] 
public class Online_skin : ScriptableObject
{
    public int face;
    public int pijama;
    public int boomerang;
    public int player;

    public void LoadCharacter(int[] data) {
        if(data.Length>= 4) {
            face = data[0];
            pijama = data[1];
            boomerang = data[2];
            player = data[3];
        }
    }
    public int[] SaveCharacter() {
        int[] data =new int[] {face,pijama,boomerang};

        return data;
    }

    public void SetPlayer(int p) {
        player = p;
    }
    
}
