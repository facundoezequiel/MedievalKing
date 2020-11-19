using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackReloadParticles : MonoBehaviour {

    void Start () {
        Invoke ("Destruir", 2f);
    }

    private void Destruir () {
        Destroy (this.gameObject);
    }
}