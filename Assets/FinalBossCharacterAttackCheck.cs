using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCharacterAttackCheck : MonoBehaviour {
    public FinalBoss finalBoss;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            finalBoss.finalBossAttackingZone = true;
            if (collider.tag == "SuperPower") {
                finalBoss.finalBossLive = 0;
            }
        }
        if (collider.tag == "CharacterAttackZone") {
            finalBoss.characterAttackingZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character" || collider.tag == "SuperPower") {
            finalBoss.finalBossAttackingZone = false;
        }
        if (collider.tag == "CharacterAttackZone") {
            finalBoss.characterAttackingZone = false;
        }
    }
}