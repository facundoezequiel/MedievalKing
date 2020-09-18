using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public Character character;
    // Move
    public float moveSpeed = 10;
    public float jumpForce = 10;
    // Live
    public float characterLive = 500;
    public float characterMaxLive = 500;
    public float liveRegenerateVelocity = 1f;
    public float liveRegeneratePoints = 1;
    public bool characterDie = false;
    // Attack
    public bool characterAttack = false;
    public int characterMinForce = 2;
    public int characterMaxForce = 8;
    public int characterForce = 7;
    // Defense
    public float characterResistence = 5;

    void Start () {
        // Character live function
        liveDieRegeneration ();
        // Frist random character force on start
        characterForce = Random.Range (characterMinForce, characterMaxForce);
    }

    void Update () {
        if (characterLive < 0) {
            characterDie = true;
        }
    }

    public void liveDieRegeneration () {
        if (characterLive < characterMaxLive && characterDie == false) {
            characterLive = characterLive + liveRegeneratePoints;
        }
        Invoke ("liveDieRegeneration", liveRegenerateVelocity);
    }
}