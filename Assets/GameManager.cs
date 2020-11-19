using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Character character;
    public FinalBoss finalBoss;
    public bool bossEnterEscapePoint = false;
    public bool levelComplete = false;
    public bool gameOver = false;
    public states state;
    public enum states {
        PLAYING,
        GAMEOVER,
        LEVELCOMPLETE
    }

    void Update () {
        statesManager ();
    }

    public void statesManager () {
        if (levelComplete == false && character.stats.characterLive > 0) {
            if (character.stats.characterLive <= 0) {
                state = states.GAMEOVER;
                gameOver = true;
            } else if (bossEnterEscapePoint == true) {
                state = states.LEVELCOMPLETE;
                levelComplete = true;
            } else {
                state = states.PLAYING;
                levelComplete = false;
                gameOver = false;
            }
        }
    }
}