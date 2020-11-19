using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public Character character;
    public CameraFollow camerafollow;
    public bool onGround = false;
    public int totalHits = 0;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Ground" || collider.tag == "Enemy") {
            totalHits++;
            onGround = true;
            camerafollow.yBlock = false;
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Ground" || collider.tag == "Enemy") {
            totalHits--;
            if (totalHits < 1) {
                onGround = false;
                camerafollow.yBlock = true;
            }
        }
    }
}