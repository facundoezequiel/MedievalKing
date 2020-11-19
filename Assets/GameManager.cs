using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Character character;
    public FinalBoss finalBoss;
    public bool bossEnterEscapePoint = false;
    public bool levelComplete = false;
    public bool gameOver = false;
    public states state;
    // Canvas
    public Text TitleMuerto;
    public Text TitleTerminado;
    public Text TextoMuerto;
    public Text TextoTerminado;
    public Button Replay;
    public enum states {
        PLAYING,
        GAMEOVER,
        LEVELCOMPLETE
    }

    void Update () {
        statesManager ();
    }

    public void statesManager () {
        if (character.stats.characterLive < 1) {
            state = states.GAMEOVER;
            gameOver = true;
            TitleMuerto.gameObject.SetActive (true);
            TextoMuerto.gameObject.SetActive (true);
            Replay.gameObject.SetActive (true);
        } else if (bossEnterEscapePoint == true) {
            state = states.LEVELCOMPLETE;
            levelComplete = true;
            TitleTerminado.gameObject.SetActive (true);
            TextoTerminado.gameObject.SetActive (true);
            Replay.gameObject.SetActive (true);
        } else {
            state = states.PLAYING;
            levelComplete = false;
            gameOver = false;
            TitleMuerto.gameObject.SetActive (false);
            TextoMuerto.gameObject.SetActive (false);
            TitleTerminado.gameObject.SetActive (false);
            TextoTerminado.gameObject.SetActive (false);
            Replay.gameObject.SetActive (false);
        }
    }
}