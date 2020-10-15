using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Character character;
    public int FireForce = 30;

    private void Update () {
        if (character.stats.characterOnFire == true && character.stats.characterDie == true) {
            character.actions.ShowFloatingText ();
        }
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            character.stats.characterOnFire = true;
            Quemar ();
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character") {
            character.stats.characterOnFire = false;
        }
    }

    public void Quemar () {
        if (character.stats.characterOnFire == true && character.stats.characterDie == false) {
            character.stats.characterLive = character.stats.characterLive - FireForce;
            character.actions.showFireForceInText = FireForce;
            character.actions.ShowFloatingText ();
            character.actions.animations.hurtAnimation ();
            Invoke ("Quemar", 1f);
        }
    }
}