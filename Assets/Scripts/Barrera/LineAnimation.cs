using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private Texture[] textures;
    int animSteps;
    [SerializeField] float fps = 30;

    float counter;

   
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= 1 / fps)
        {
            animSteps++;
            if(animSteps == textures.Length)
            {
                animSteps = 0;
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animSteps]);
            counter = 0;
        }
        
    }
}
