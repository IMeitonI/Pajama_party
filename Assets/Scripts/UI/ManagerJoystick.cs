using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManagerJoystick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Image joystick_BG, joystick;
    private Vector2 pos_input;

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick_BG.rectTransform,eventData.position,eventData.pressEventCamera, out pos_input))
        {
            pos_input.x = pos_input.x / (joystick_BG.rectTransform.sizeDelta.x);
            pos_input.y = pos_input.y / (joystick_BG.rectTransform.sizeDelta.y);

            //Normalize Pos Input
            if (pos_input.magnitude > 1f)
            {
                pos_input = pos_input.normalized;
            }

            //JoyStick Move
            joystick.rectTransform.anchoredPosition = new Vector2(pos_input.x*joystick_BG.rectTransform.sizeDelta.x/2, pos_input.y*joystick_BG.rectTransform.sizeDelta.y/2);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pos_input = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick_BG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

   public float InputHorizontal()
    {
        if (pos_input.x != 0) return pos_input.normalized.x;
        else return Input.GetAxis("Horizontal");
    }
    public float InputVertical()
    {
        if (pos_input.y != 0) return pos_input.normalized.y;
        else return Input.GetAxis("Vertical");
    }
}
