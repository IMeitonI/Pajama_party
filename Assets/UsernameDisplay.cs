using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView playerPV;
    [SerializeField] SkinData m_skin;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.text = playerPV.Owner.NickName +" ("+m_skin.face.ToString()+m_skin.pijama.ToString()+m_skin.boomerang.ToString()+")";
    }

}
