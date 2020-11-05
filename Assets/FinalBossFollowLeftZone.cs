using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossFollowLeftZone : MonoBehaviour {
    public FinalBoss finalBoss;
    Quaternion rotation;

    void Awake () {
        rotation = transform.rotation;
    }

    void LateUpdate () {
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character" && finalBoss.finalBossAttackingZone == false) {
            finalBoss.characterEnterInLeftZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character") {
            finalBoss.characterEnterInLeftZone = false;
        }
    }
}