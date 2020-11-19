using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public FinalBoss finalBoss;

    public void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "CharacterAttackCheck") {
            if (finalBoss.bossEscape == true) {
                print ("destruido");
                Destroy (this.gameObject);
            }
        }
    }
}