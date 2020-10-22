using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Character character;
    public int FireForce;
    public int FireForceMin = 20;
    public int FireForceMax = 40;

    private void Start () {
        RandomFireForce ();
    }

    private void Update () {
        if (character.stats.characterOnFire == true && character.stats.characterDie == true) {
            character.stats.characterOnFire = false;
        }
    }

    public void RandomFireForce () {
        FireForce = Random.Range (FireForceMin, FireForceMax);
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            character.stats.characterOnFire = true;
            Quemar ();
            RandomFireForce ();
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
            FireForce = Random.Range (FireForceMin, FireForceMax);
            Invoke ("Quemar", 1f);
        }
    }
}