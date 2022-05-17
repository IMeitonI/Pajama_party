using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnSoundsManager : MonoBehaviour
{
    [SerializeField] TMP_Text offText;
    [SerializeField] TMP_Text onText;
    [SerializeField]  Image togglImage;
    [Header("sprites")]
    [SerializeField]  Sprite onSprite,offSprite;
    [Header("colors")]
    [SerializeField]  Color onColor,offColor;
    [SerializeField] Button button;
    
   
    public void ChargeMute()
    {
        managerSound.Instance.ChargeMute();
    }

    private void Start()
    {

        //button.onClick += managerSound.Instance.ChargeMute();
        
        if (Save_Manager.saveM_instance.activeSave.muted)
        {
            togglImage.sprite = offSprite;
           if(offText!= null|| onText !=null )
            {
                offText.color = offColor;
                onText.color = onColor;
            }
        }
        else
        {
            togglImage.sprite = onSprite;
           
            if (offText != null || onText != null)
            {
                offText.color = onColor;
                onText.color = offColor;
            }
        }
    }

    public void onSound()
    {
        if(Save_Manager.saveM_instance.activeSave.muted)
        {
            togglImage.sprite = onSprite;
            if (offText != null || onText != null)
            {
               
                offText.color = onColor;
                onText.color = offColor;
            }

            Save_Manager.saveM_instance.activeSave.muted = false;
            Debug.Log("sound is Off");

        }
        else
        {
            togglImage.sprite = offSprite;
            if (offText != null || onText != null)
            {
                offText.color = offColor;
                onText.color = onColor;
            }

            Save_Manager.saveM_instance.activeSave.muted = true;
            Debug.Log("sound is On");

        }
        Save_Manager.saveM_instance.Save();
    }


}
