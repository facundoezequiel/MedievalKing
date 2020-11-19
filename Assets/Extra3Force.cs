using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra3Force : MonoBehaviour {
    public PowerUpsManager manager;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            manager.extra3Force = true;
            manager.ActivarPowerUps ();
            Destroy (this.gameObject);
        }
    }
}