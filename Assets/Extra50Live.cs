using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra50Live : MonoBehaviour {
    public PowerUpsManager manager;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character") {
            manager.extra50Live = true;
            manager.ActivarPowerUps ();
            Destroy (this.gameObject);
        }
    }
}