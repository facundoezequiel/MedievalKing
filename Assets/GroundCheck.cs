using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public Character character;
    public CameraFollow camerafollow;
    public bool onGround = false;

    // Character on ground
    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Ground") {
            onGround = true;
            camerafollow.yBlock = false;
        }
    }

    // Character out ground
    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.tag == "Ground") {
            onGround = false;
            camerafollow.yBlock = true;
        }
    }
}