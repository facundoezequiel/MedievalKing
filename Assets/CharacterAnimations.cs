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
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isWalking", true);
    }

    // Jump animation
    public void jumpAnimation () {
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isJumping", true);
    }

    // IDLE animation
    public void idleAnimation () {
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking", false);
    }

    // Attack animation
    public void attackAnimation () {
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isAttacking", true);
    }

    // Hurt animation
    public void hurtAnimation () {
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isHurt", true);
    }

    // Die animation
    public void dieAnimation () {
        anim.SetBool ("isJumping", false);
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isDying", true);
    }
}