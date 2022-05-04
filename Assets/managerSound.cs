using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerSound : MonoBehaviour
{
    Movement mov;
    ManagerJoystick mj;
    public AudioSource dieSound;
    public AudioSource soundMovement;
    public AudioSource soundShooting;
    public AudioSource soundRebote;
    public AudioSource soundButtonSettings;
    public AudioSource soundPrincipalMenu;
    public Button boton;
  
    // Start is called before the first frame update
    void Start()
    {
        dieSound = GetComponent<AudioSource>();
        soundMovement = GetComponent<AudioSource>();
        soundShooting = GetComponent<AudioSource>();
        soundRebote = GetComponent<AudioSource>();
        soundButtonSettings = GetComponent<AudioSource>();
        soundPrincipalMenu = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void soundDie ()
    {
        
        if(dieSound.isPlaying == false)
        {
            dieSound.Play();
        }
    }

    public void soundMove()
    {
        /* if(soundMovement.isPlaying == false)
        {
            soundMovement.Play();
        }
        */
        soundMovement.Play();
        
    }


    public void soundShoot()
    {
        soundShooting.Play();
        
    }

    public void soundReboting()
    {
        soundRebote.Play();
    }

    /* public void buttonSettings()
     {
         soundButtonSettings.Play();
     }*/

    public void muteSettings()
    {
       
        
        
    }
    

    public void soundMainMenu()
    {
        soundPrincipalMenu.Play();
    }

    
    
}
