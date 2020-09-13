using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : MonoBehaviour {
    public float blackEnemyLive = 40;

    void Start () {
        blackEnemyIdle ();
    }

    void Update () {
        if (blackEnemyLive == 0) {
            blackEnemyDie ();
        }
    }

    public void blackEnemyIdle () {

    }

    public void blackEnemyDie () {

    }
}