using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerArea : MonoBehaviour {
    public Character character;
    public Animator anim;

    void Start () {
        anim = GetComponent<Animator> ();
        resetScale();
    }

    public void playAnimation () {
        anim.SetBool ("isActive", true);
        character.stats.superPowerActive = true;
        Invoke ("resetScale", 1.2f);
    }

    private void resetScale () {
        character.stats.superPowerActive = false;
        anim.SetBool ("isActive", false);
    }
}
