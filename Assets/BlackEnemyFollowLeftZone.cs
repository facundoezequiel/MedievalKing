using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemyFollowLeftZone : MonoBehaviour {
    public BlackEnemy blackEnemy;
    Quaternion rotation;

    void Awake () {
        rotation = transform.rotation;
    }
    
    void LateUpdate () {
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Character" && blackEnemy.characterAttackingZone == false) {
            blackEnemy.characterEnterInLeftZone = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Character") {
            blackEnemy.characterEnterInLeftZone = false;
        }
    }
}