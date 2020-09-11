using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour {
    public Character character;
    public CharacterAnimations animations;
    public GroundCheck groundcheck;
    public states state;
    public enum states {
        IDLE,
        WALKING,
        JUMPING
    }

    // Character IDLE function
    public void Idle () {
        // State
        state = states.IDLE;
        // Animations
        animations.idleAnimation ();
    }

    // Character Walk function
    public void Walk (int direction) {
        Vector3 movement = new Vector3 (direction, 0f, 0f);
        transform.position += movement * Time.deltaTime * character.stats.moveSpeed;
        // Character direction rotation
        if (direction == 1) {
            // Right
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else {
            // Left
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }
        // If character on ground
        if (groundcheck.onGround == true) {
            // Animation
            animations.walkAnimation ();
            // State
            state = states.WALKING;
        } else {
            // Animation
            animations.jumpAnimation ();
            // State
            state = states.JUMPING;
        }
    }

    // Character Jump function
    public void Jump () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, character.stats.jumpForce), ForceMode2D.Impulse);
        // State
        state = states.JUMPING;
        // Animations 
        animations.jumpAnimation ();
    }
}