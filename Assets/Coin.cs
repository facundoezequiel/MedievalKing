using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public Character character;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            character.stats.takeCoin = true;
            character.stats.coins = character.stats.coins + 1;
            character.actions.ShowFloatingText ();
            Destroy (this.gameObject);
        }
    }
}