using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] protected ManagerJoystick managerJoystick;
    protected Transform player;
    protected Vector2 direction;
    public bool death;
    GroundCheckl check;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        check = GetComponent<GroundCheckl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (managerJoystick == null || check.grounded == false) return;
        direction.x = managerJoystick.InputHorizontal();
        direction.y = managerJoystick.InputVertical();
        if (direction!= Vector2.zero && death == false)
        {
            float angle = Mathf.Atan2(direction.y - Vector2.zero.y, direction.x - Vector2.zero.x);
            player.rotation = Quaternion.Euler(0f, 90- angle*Mathf.Rad2Deg, 0f);
        }
    }
}
