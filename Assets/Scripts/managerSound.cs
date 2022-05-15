using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class managerSound : MonoBehaviour
{
    Movement mov;
    //ManagerJoystick mj;
    public AudioSource EffectSource;
    public AudioSource MusicSource;

    public static managerSound Instance = null;

    //public AudioSource dieSound;
    //public AudioSource soundMovement;
    //public AudioSource soundShooting;
    //public AudioSource soundRebote;
    //public AudioSource soundButtonSettings;
    //public AudioSource soundPrincipalMenu;
    //public Button boton;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        ChargeMute();
    }
    private void Start()
    {
        ChargeMute();
    }
    public void ChargeMute()
    {

        if (Save_Manager.saveM_instance.activeSave.muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

    }

    public void Play(AudioClip clip)
    {
        EffectSource.clip = clip;
        EffectSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    dieSound = GetComponent<AudioSource>();
    //    soundMovement = GetComponent<AudioSource>();
    //    soundShooting = GetComponent<AudioSource>();
    //    soundRebote = GetComponent<AudioSource>();
    //    soundButtonSettings = GetComponent<AudioSource>();
    //    soundPrincipalMenu = GetComponent<AudioSource>();
      
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

  

    //public void soundDie ()
    //{
        
    //    if(dieSound.isPlaying == false)
    //    {
    //        dieSound.Play();
    //    }
    //}

    //public void soundMove()
    //{
    //    /* if(soundMovement.isPlaying == false)
    //    {
    //        soundMovement.Play();
    //    }
    //    */
    //    soundMovement.Play();
        
    //}


    //public void soundShoot()
    //{
    //    soundShooting.Play();
        
    //}

    //public void soundReboting()
    //{
    //    soundRebote.Play();
    //}

    ///* public void buttonSettings()
    // {
    //     soundButtonSettings.Play();
    // }*/

    //public void muteSettings()
    //{
       
        
        
    //}
    

    //public void soundMainMenu()
    //{
    //    soundPrincipalMenu.Play();
    //}

    
    
}
