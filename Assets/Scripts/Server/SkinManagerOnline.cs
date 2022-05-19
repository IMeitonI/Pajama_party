using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class SkinManagerOnline : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer m_meshRend_face;
    [SerializeField] SkinnedMeshRenderer m_meshRend_body;
    [SerializeField] SkinnedMeshRenderer m_meshRend_tail;
    [SerializeField] MeshFilter m_meshRend_Boomerang;

    [SerializeField] faceOBJ[] m_faceList;
    [SerializeField] bodyOBJ[] m_bodyList;
    [SerializeField] tailOBJ[] m_tailList;
    [SerializeField] BoomerangOBJ[] m_BoomerangList;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        Save_Manager.saveM_instance.Load();

        LoadMeshOnline((int)pv.Owner.CustomProperties["Face"], (int)pv.Owner.CustomProperties["Body"], (int)pv.Owner.CustomProperties["Boomerang"]);
        
    }

    public void LoadMeshOnline(int face, int body, int boomerang)
    {
        

        m_meshRend_face.sharedMesh = (m_faceList[face].m_mesh);
        m_meshRend_tail.sharedMesh = (m_tailList[face].m_mesh);
        m_meshRend_body.sharedMesh = (m_bodyList[body].m_mesh);
        //m_meshRend_Boomerang.mesh = (m_BoomerangList[boomerang].m_mesh);

        m_meshRend_face.materials = (m_faceList[face].m_material);
        m_meshRend_body.materials = (m_bodyList[body].m_material);
        m_meshRend_tail.materials = (m_tailList[face].m_material);
    }
}
