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
        state = states.PLAYING;
        levelComplete = false;
        gameOver = false;
        gameOverUI.gameObject.SetActive (false);
        gameTerminadoUI.gameObject.SetActive (false);
        segundos = 0;
        minutos = 0;
        timeManager ();
    }

    void Update () {
        statesManager ();
    }

    public void statesManager () {
        if (character.stats.characterLive < 1) {
            state = states.GAMEOVER;
            gameOver = true;
            gameOverUI.gameObject.SetActive (true);
            gameTerminadoUI.gameObject.SetActive (false);
            calcularPuntaje();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            tablaDePuntaje.AddPuntajeEntrada(puntaje, playerName);
        } else if (bossEnterEscapePoint == true) {
            state = states.LEVELCOMPLETE;
            levelComplete = true;
            gameOverUI.gameObject.SetActive (false);
            gameTerminadoUI.gameObject.SetActive (true);
            calcularPuntaje();
            // Agrega una nueva entrada de puntaje con el puntaje y nombre de la partida actual
            tablaDePuntaje.AddPuntajeEntrada(puntaje, playerName);
        }
    }

    public void timeManager () {
        mostrarTiempo();
        if (state == states.PLAYING) {
            Invoke("addSecond", 1);
        }
    }

    public void mostrarTiempo() {
        if (segundos < 10 && minutos < 10) {
            TimeText.text = "0" + minutos.ToString() + ":" + "0" + segundos.ToString();
        } 
        if (segundos >= 10 && minutos < 10) {
            TimeText.text = "0" + minutos.ToString() + ":" + segundos.ToString();
        }
        if (segundos >= 10 && minutos >= 10) {
            TimeText.text = minutos.ToString() + ":" + segundos.ToString();
        }
        if (segundos < 10 && minutos >= 10) {
            TimeText.text = minutos.ToString() + ":" + "0" + segundos.ToString();
        }
    }

    public void addSecond () {
        segundos++;
        if (segundos == 60) {
            segundos = 0;
            minutos++;
        }
        timeManager();
    }

    public void calcularPuntaje() {
        if (minutos <= 1) {
            puntaje = puntaje + 1000;
        }
        if (minutos == 2) {
            puntaje = puntaje + 500;
        }
        if (minutos == 3) {
            puntaje = puntaje + 200;
        }
        if (minutos >= 4) {
            return;
        }
    }
}