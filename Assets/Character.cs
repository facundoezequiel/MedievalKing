using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterActions actions;
    public CharacterStats stats;
    public InputManager input;

    void Start () {
        actions.Idle ();
    }
}