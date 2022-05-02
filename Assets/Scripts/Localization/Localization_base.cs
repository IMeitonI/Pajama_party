using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization_base : MonoBehaviour
{
    public delegate void TranslateEvent();
    public event TranslateEvent TranslateLoad;

    public static Localization_base instance;
    public string language;
    public static Dictionary<string, string> dataText_ENG = new Dictionary<string, string>
    {
            { "play", "Play" },
            { "quit_lobby", "Quit Lobby" },
            { "leave_room", "Leave Room" },
            { "salirjuego", "Quit" },
            { "find_match", "Find Match" },
            { "local_vs", "Local VS" },
            { "quit_game", "Quit Game" },
            { "hello", "Hello" },
            { "start_game","Start Game" },
            { "loading","Loading..." },
            { "online_multiplayer","Online Multiplayer" },
            { "back","Back" },
            { "controls","Controls" },
            { "lang","Language" },
            { "sound","Sound" },
            { "settings","Settings" },
            { "move","Move" },
            { "throw","Throw" },
            { "dash","Dash" },
            { "winner","Winner!" },
            { "loser","Loser" },
            { "win","Win" },
            { "create_room","Create Room" },
            { "find_room","Find Room" },
            { "start","Start" },
            { "room_name","Room Name" },
            { "create","Create" },
            { "number_of_kills","Number of Kills" },
            { "number_of_kills_for_win","Number of kills for win" },
            { "confirm","Confirm" },
            { "on","On" },
            { "off","Off" },
            { "selection_character","Selection Character" },
            { "customizacion","Customizacion" },
            { "pijama","Pijama" },
            { "Enter_room_name","Enter room name" },
            { "nick_name","nickname" },
            { "enter_your_name","Enter your nickname" },
            { "customise","Customise" },


    };

    public static Dictionary<string, string> dataText_SP = new Dictionary<string, string>
    {
            { "play", "Jugar" },
            { "quit_lobby", "Salir del lobby" },
            { "leave_room", "Dejar la sala" },
            { "salirjuego", "Salir" },
            { "find_match", "Buscar Partida" },
            { "local_vs", "Batalla Local" },
            { "quit_game", "Salir del juego" },
            { "hello", "Hola" },
            { "start_game","Empezar Juego" },
            { "loading","Cargando..." },
            { "online_multiplayer","Multijugdor en linea" },
            { "back","Volver" },
            { "controls","Controles" },
            { "lang","Idioma" },
            { "sound","Sonido" },
            { "settings","Configuraciones" },
            { "move","Mover" },
            { "throw","Lanzar" },
            { "dash","Turbo" },
            { "winner","Ganador" },
            { "loser","Perdedor" },
            { "win","Ganador" },
            { "create_room","Crear Sala" },
            { "find_room","Buscar Sala" },
            { "start","Comenzar" },
            { "room_name","Nombre sala" },
            { "create","Crear" },
            { "number_of_kills","Número de muertes" },
            { "number_of_kills_for_win","Número de muertes para ganar" },
            { "confirm","Confirmar" },
            { "on","Encendido" },
            { "off","Apagado" },
            { "selection_character","Selecciòn Personaje" },
            { "customizacion","Personalizacion" },
            { "pijama","Pijama" },
            { "Enter_room_name","Ingrese el nombre de la sala" },
            { "nick_name","apodo" },
            { "enter_your_name","Ingresa tu apodo" },
            { "customise","Personalizar" },

    };
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(language == null)
        {
            language = "SP";
        }
        else
        {  
            language = Save_Manager.saveM_instance.activeSave.language;
            if (TranslateLoad != null) TranslateLoad();
        }

        
    }


    public string GetTraslation(string key)
    {
        string translate = "";
        if (language == "SP")
        {
            translate = dataText_SP[key];
        }
        else if (language == "ENG")
        {
            translate = dataText_ENG[key];

        }
        return translate;
    }

}