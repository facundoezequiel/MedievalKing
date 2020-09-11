using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {
    public Animator anim;

    void Start () {
        anim = GetComponent<Animator> ();
    }

    // Walk animation
    public void walkAnimation () {
        anim.SetBool ("isWalking", true);
        anim.SetBool ("isJumping", false);
    }

    // Jump animation
    public void jumpAnimation () {
        anim.SetBool ("isJumping", true);
        anim.SetBool ("isWalking", false);
    }

    // IDLE animation
    public void idleAnimation () {
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isWalking", false);
    }
}