using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Character character;
    public FinalBoss finalBoss;
    public TablaDePuntaje tablaDePuntaje;
    public bool jugandoPartida = false;
    public bool terminoPartida = false;
    public bool bossEnterEscapePoint = false;
    public bool levelComplete = false;
    public bool gameOver = false;
    public states state;
    // Time
    public int minutos = 0;
    public int segundos = 0;
    // Puntaje partida actual
    public string playerName;
    public int puntaje = 0;
    // Canvas
    public GameObject gameOverUI;
    public GameObject gameTerminadoUI;
    public NameTransfer nameInputContainer;
    // Playing UI
    public Text TimeText;
    public Text HeartText;
    public Text CoinsText;
    public Text ManaText;
    public Image HeartImage;
    public Image CoinsImage;
    public Image ManaImage;
    public GameObject joystick;
    // States
    public enum states {
        PLAYING,
        GAMEOVER,
        LEVELCOMPLETE,
        INSERTNAME,
    }

    void Start () {
        // Cunado comienza el juego, primero debe poner el nombre
        state = states.INSERTNAME;
        // El nivel no esta terminado
        levelComplete = false;
        // El juegador no perdio
        gameOver = false;
        // Desactivo la UI de juego perdido
        gameOverUI.gameObject.SetActive (false);
        // Desactivo la UI de juego terminado
        gameTerminadoUI.gameObject.SetActive (false);
        // Desactivo la tabla de puntajes
        tablaDePuntaje.gameObject.SetActive (false);
        // Muestro la UI del input para completar el nombre
        nameInputContainer.gameObject.SetActive (true);
        // Pongo los segundos y lo minutos en 0
        segundos = 0;
        minutos = 0;
        // Desactivo la UI de playing
        TimeText.gameObject.SetActive (false);
        ManaText.gameObject.SetActive (false);
        CoinsText.gameObject.SetActive (false);
        HeartText.gameObject.SetActive (false);
        ManaImage.gameObject.SetActive (false);
        CoinsImage.gameObject.SetActive (false);
        HeartImage.gameObject.SetActive (false);
        // Desactivo el joystick
        joystick.gameObject.SetActive (false);
        // Pongo el boleano de que la partida termino en false
        terminoPartida = false;
    }

    void Update () {
        // Llamo a la funcion que controla los estados del juego para que los chequee constantemente
        statesManager ();
    }

    // Funcion que controla los estados del juego
    public void statesManager () {
        // Si se completo el input del nombre
        if (nameInputContainer.inputCompletado == true && terminoPartida == false) {
            // Comienza el juego, por lo tanto el estado pasa a JUGANDO
            state = states.PLAYING;
            // Pongo el boleano del input completado en false para que no entre todo el tiempo el if
            nameInputContainer.inputCompletado = false;
            // Desactivo el input UI
            nameInputContainer.gameObject.SetActive (false);
            // Activo la UI del playing
            TimeText.gameObject.SetActive (true);
            ManaText.gameObject.SetActive (true);
            CoinsText.gameObject.SetActive (true);
            HeartText.gameObject.SetActive (true);
            ManaImage.gameObject.SetActive (true);
            CoinsImage.gameObject.SetActive (true);
            HeartImage.gameObject.SetActive (true);
            // Activo el joystick
            joystick.gameObject.SetActive (true);
            // Llammo la funcion que controla el tiempo asi empieza a correr
            timeManager ();
            // Pongo el boleano de jugando en true asi se activan los inputs
            jugandoPartida = true;
        }
        // Si el jugador perdio toda la vida
        if (character.stats.characterLive < 1 && terminoPartida == false) {
            // El estado pasa estar en JUEGO PERDIDO
            state = states.GAMEOVER;
            // El juegador perdio
            gameOver = true;
            // Muestro la UI que corresponde
            gameOverUI.gameObject.SetActive (true);
            gameTerminadoUI.gameObject.SetActive (false);
            nameInputContainer.gameObject.SetActive (false);
            // Desactivo la UI de playing
            TimeText.gameObject.SetActive (false);
            ManaText.gameObject.SetActive (false);
            CoinsText.gameObject.SetActive (false);
            HeartText.gameObject.SetActive (false);
            ManaImage.gameObject.SetActive (false);
            CoinsImage.gameObject.SetActive (false);
            HeartImage.gameObject.SetActive (false);
            // Desactivo el joystick
            joystick.gameObject.SetActive (false);
            // Llamo funcion 
            calcularPuntaje ();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            // Activo la tabla
            tablaDePuntaje.gameObject.SetActive (true);
            // Pongo el boleano de jugando en false
            jugandoPartida = false;
            terminoPartida = true;
        } else if (bossEnterEscapePoint == true && terminoPartida == false) {
            state = states.LEVELCOMPLETE;
            levelComplete = true;
            gameOverUI.gameObject.SetActive (false);
            nameInputContainer.gameObject.SetActive (false);
            gameTerminadoUI.gameObject.SetActive (true);
            // Desactivo la UI de playing
            TimeText.gameObject.SetActive (false);
            ManaText.gameObject.SetActive (false);
            CoinsText.gameObject.SetActive (false);
            HeartText.gameObject.SetActive (false);
            ManaImage.gameObject.SetActive (false);
            CoinsImage.gameObject.SetActive (false);
            HeartImage.gameObject.SetActive (false);
            // Desactivo el joystick
            joystick.gameObject.SetActive (false);
            // Le sumo puntos por ganar
            puntaje = puntaje + 500000;
            calcularPuntaje ();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            // Activo la tabla
            tablaDePuntaje.gameObject.SetActive (true);
            // Pongo el boleano de jugando en false
            jugandoPartida = false;
            terminoPartida = true;
        }
    }

    // Funcion que controla el tiempo
    public void timeManager () {
        // Llama a la funcion que muestra el tiempo en pantalla
        mostrarTiempo ();
        // Si solamente el estado de juego esta en JUGANDO
        if (state == states.PLAYING) {
            // Llama a la funcion que agrega un segundo en un segundo (luego esa funcion vuelve a llamar a esta y asi corre el tiempo a base de un loop)
            Invoke ("addSecond", 1);
        }
    }

    // Funcion que muestra el tiempo en pantalla
    public void mostrarTiempo () {
        // Si los segundos y minutos son menores a 10 le agrega un 0 adelante a ambos
        if (segundos < 10 && minutos < 10) {
            // Muestro el tiempo en pantalla
            TimeText.text = "0" + minutos.ToString () + ":" + "0" + segundos.ToString ();
        }
        // Si los minutos son menores a 10 y los segundos son mayores o iguales a 10, le poongo un 0 adelante solo a a los minutos
        if (segundos >= 10 && minutos < 10) {
            // Muestro el tiempo en pantalla
            TimeText.text = "0" + minutos.ToString () + ":" + segundos.ToString ();
        }
        // Si los segundos y los minutos son mayores o iguales a 10, no pongo ningun 0
        if (segundos >= 10 && minutos >= 10) {
            // Muestro el tiempo en pantalla
            TimeText.text = minutos.ToString () + ":" + segundos.ToString ();
        }
        // Si los segundos son menores a 10 y los minutos son mayores o iguales a 10, le poongo un 0 adelante solo a a los segundos
        if (segundos < 10 && minutos >= 10) {
            TimeText.text = minutos.ToString () + ":" + "0" + segundos.ToString ();
        }
    }

    // Funcion que agrega un segundo al tiempo
    public void addSecond () {
        // Suma un segundo
        segundos++;
        // Si los segundos llegan a 60, suma un minuto y pone los segundos en 0
        if (segundos == 60) {
            segundos = 0;
            minutos++;
        }
        // LLama a la funcion que controla el tiempo para que vuelva a llamar a esta funcion en un segundo
        timeManager ();
    }

    // Funcion que calcula el puntaje en base al tiempo de partida
    public void calcularPuntaje () {
        // Si el juego no termino por muerte del personaje
        if (state != states.GAMEOVER) {
            // Si tardo menos de dos minutos
            if (minutos <= 1) {
                // Suma 1000 puntos
                puntaje = puntaje + 500000;
            }
            // Si tardo menos de tres minutos
            if (minutos == 2) {
                // Suma 500 puntos
                puntaje = puntaje + 300000;
            }
            // Si tardo menos de cuatro minutos
            if (minutos == 3) {
                // Suma 200 puntos
                puntaje = puntaje + 100000;
            }
            // Si tardo 4 o mas minutos
            if (minutos >= 4) {
                // No suma puntos
                return;
            }
        }
        // Si el juego termina porque el personaje murio, no suma puntos
        else {
            // No suma puntos
            return;
        }
    }
}