using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public Character character;
    public Animator CoinAnim;
    public GameManager gameManager;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            gameManager.puntaje = gameManager.puntaje + 25000;
            CoinAnim.Play ("Base Layer.CoinImage", 0, 0.25f);
            character.stats.takeCoin = true;
            character.stats.coins = character.stats.coins + 1;
            character.actions.ShowFloatingText ();
            Destroy (this.gameObject);
        }
    }
}