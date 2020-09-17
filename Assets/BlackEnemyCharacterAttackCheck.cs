using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemyCharacterAttackCheck : MonoBehaviour {
    public BlackEnemy blackEnemy;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            blackEnemy.characterAttackingZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character") {
            blackEnemy.characterAttackingZone = false;
        }
    }
}