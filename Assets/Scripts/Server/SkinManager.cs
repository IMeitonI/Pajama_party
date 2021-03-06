using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class faceOBJ
{
    public Mesh m_mesh;
    public Material[] m_material;
}
[System.Serializable]

public class bodyOBJ
{
    public Mesh m_mesh;
    public Material[] m_material;
}
[System.Serializable]

public class tailOBJ
{
    public Mesh m_mesh;
    public Material[] m_material;
}
[System.Serializable]

public class BoomerangOBJ
{
    public Mesh m_mesh;
    public Material m_material;
}

public class SkinManager : MonoBehaviour
{
    [SerializeField] SkinData m_skin;

    [SerializeField] SkinnedMeshRenderer m_meshRend_face;
    [SerializeField] SkinnedMeshRenderer m_meshRend_body;
    [SerializeField] SkinnedMeshRenderer m_meshRend_tail;
    [SerializeField] MeshFilter m_meshRend_Boomerang;

    [SerializeField] faceOBJ[] m_faceList;
    [SerializeField] bodyOBJ[] m_bodyList;
    [SerializeField] bodyOBJ[] m_tailList;
    [SerializeField] BoomerangOBJ[] m_BoomerangList;
    [SerializeField] GameObject[] m_BoomerangListObjs;

    [Header("skin Vars")]
    [SerializeField] int faceVar, bodyVar;

    void Start()
    {
        Save_Manager.saveM_instance.Load();
        LoadMesh();
    }

    public void LoadMeshOnline(int face, int body)
    {

        m_meshRend_face.sharedMesh = (m_faceList[face].m_mesh);
        m_meshRend_body.sharedMesh = (m_bodyList[body].m_mesh);
        m_meshRend_tail.sharedMesh = (m_tailList[face].m_mesh);
        m_meshRend_Boomerang.mesh = (m_BoomerangList[m_skin.boomerang].m_mesh);

        m_meshRend_face.materials = (m_faceList[face].m_material);
        m_meshRend_body.materials = (m_bodyList[body].m_material);
        m_meshRend_tail.materials = (m_tailList[face].m_material);

    }


    public void LoadMesh()
    {

        m_meshRend_face.sharedMesh = (m_faceList[m_skin.face].m_mesh);
        m_meshRend_body.sharedMesh = (m_bodyList[m_skin.pijama].m_mesh);
        m_meshRend_tail.sharedMesh = (m_tailList[m_skin.face].m_mesh);
        m_meshRend_Boomerang.mesh = (m_BoomerangList[m_skin.boomerang].m_mesh);

        m_meshRend_face.materials = (m_faceList[m_skin.face].m_material);
        m_meshRend_body.materials = (m_bodyList[m_skin.pijama].m_material);
        m_meshRend_tail.materials = (m_tailList[m_skin.face].m_material);

        for (int i = 0; i < m_BoomerangListObjs.Length; i++)
        {
            m_BoomerangListObjs[i].SetActive(false);
        }
        m_BoomerangListObjs[m_skin.boomerang].SetActive(true);


    }

}