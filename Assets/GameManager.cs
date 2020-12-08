using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Character character;
    public FinalBoss finalBoss;
    public TablaDePuntaje tablaDePuntaje;
    public bool bossEnterEscapePoint = false;
    public bool levelComplete = false;
    public bool gameOver = false;
    public states state;
    // Time
    public int minutos = 0;
    public int segundos = 0;
    // Puntaje partida actual
    public string playerName = "Player";
    public int puntaje = 0;
    // Canvas
    public GameObject gameOverUI;
    public GameObject gameTerminadoUI;
    public Text TimeText;
    public enum states {
        PLAYING,
        GAMEOVER,
        LEVELCOMPLETE
    }

    void Start () {
        // Cunado comienza el juego, el estado esta en JUGANDO
        state = states.PLAYING;
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
        // Pongo los segundos y lo minutos en 0
        segundos = 0;
        minutos = 0;
        // Llammo la funcion que controla el tiempo asi empieza a correr
        timeManager ();
    }

    void Update () {
        // Llamo a la funcion que controla los estados del juego para que los chequee constantemente
        statesManager ();
    }

    // Funcion que controla los estados del juego
    public void statesManager () {
        // Si el jugador perdio toda la vida
        if (character.stats.characterLive < 1) {
            // El estado pasa estar en JUEGO PERDIDO
            state = states.GAMEOVER;
            // El juegador perdio
            gameOver = true;
            // Muestro la UI que corresponde
            gameOverUI.gameObject.SetActive (true);
            gameTerminadoUI.gameObject.SetActive (false);
            // Llamo funcion 
            calcularPuntaje ();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            // Activo la tabla
            tablaDePuntaje.gameObject.SetActive (true);
        } else if (bossEnterEscapePoint == true) {
            state = states.LEVELCOMPLETE;
            levelComplete = true;
            gameOverUI.gameObject.SetActive (false);
            gameTerminadoUI.gameObject.SetActive (true);
            calcularPuntaje ();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            // Activo la tabla
            tablaDePuntaje.gameObject.SetActive (true);
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
        // Si tardo menos de dos minutos
        if (minutos <= 1) {
            // Suma 1000 puntos
            puntaje = puntaje + 60000;
        }
        // Si tardo menos de tres minutos
        if (minutos == 2) {
            // Suma 500 puntos
            puntaje = puntaje + 500;
        }
        // Si tardo menos de cuatro minutos
        if (minutos == 3) {
            // Suma 200 puntos
            puntaje = puntaje + 200;
        }
        // Si tardo 4 o mas minutos
        if (minutos >= 4) {
            // No suma puntos
            return;
        }
    }
}