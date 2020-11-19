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
    public GameObject gameOverUI;
    public GameObject gameTerminadoUI;
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
        } else if (bossEnterEscapePoint == true) {
            state = states.LEVELCOMPLETE;
            levelComplete = true;
            gameOverUI.gameObject.SetActive (false);
            gameTerminadoUI.gameObject.SetActive (true);
        }
    }
}