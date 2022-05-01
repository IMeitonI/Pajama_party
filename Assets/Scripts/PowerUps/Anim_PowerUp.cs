using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_PowerUp : MonoBehaviour
{
    [SerializeField] AnimationCurve animationCurve_Rot,animationCurve_Pos;
    [SerializeField] float duration;
    private float time,t;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        t = time / duration;
        transform.position = new Vector3(transform.position.x, transform.position.y+animationCurve_Pos.Evaluate(t), transform.position.z);
        transform.rotation = Quaternion.Euler(45,359 * animationCurve_Rot.Evaluate(t), transform.rotation.z);
        if (t >= 1) time = 0;
    }
}
