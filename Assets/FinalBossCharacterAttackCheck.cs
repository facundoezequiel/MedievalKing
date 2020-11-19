using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossCharacterAttackCheck : MonoBehaviour {
    public FinalBoss finalBoss;
    public bool characterSuperPower = false;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            finalBoss.finalBossAttackingZone = true;
            if (collider.tag == "SuperPower") {
                finalBoss.anim.SetBool ("isWalking", false);
                finalBoss.anim.SetBool ("isHurt", true);
                finalBoss.anim.SetBool ("isAttacking1", false);
                finalBoss.anim.SetBool ("isAttacking2", false);
                finalBoss.anim.SetBool ("isAttacking3", false);
                finalBoss.anim.SetBool ("isBlock", false);
                finalBoss.anim.SetBool ("isDying", false);
                finalBoss.finalBossLive = finalBoss.finalBossLive - 100;
                characterSuperPower = true;
                finalBoss.ShowFloatingText ();
            }
        }
        if (collider.tag == "CharacterAttackZone") {
            finalBoss.characterAttackingZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            finalBoss.finalBossAttackingZone = false;
            characterSuperPower = false;
        }
        if (collider.tag == "CharacterAttackZone") {
            finalBoss.characterAttackingZone = false;
        }
    }
}