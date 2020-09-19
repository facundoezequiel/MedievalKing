using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour {
    public AudioSource coinSound;
    public Character character;
    public GameObject FloatingTextPrefab;
    public CharacterAnimations animations;
    public Text CoinsText;
    public float showLiveInText;
    public GroundCheck groundcheck;
    public states state;
    public enum states {
        IDLE,
        WALKING,
        JUMPING,
        HURT,
        ATTACKING,
        DEATH
    }

    public void ShowFloatingText () {
        var FloatingText = Instantiate (FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        if (character.stats.takeCoin == true) {
            FloatingText.GetComponent<TextMesh> ().color = Color.yellow;
            FloatingText.GetComponent<TextMesh> ().text = "+1";
            CoinsText.text = character.stats.coins.ToString ();
            coinSound.Play (0);
        }
        if (character.stats.characterLive > 0 && character.stats.takeCoin == false) {
            if (showLiveInText != 0) {
                FloatingText.GetComponent<TextMesh> ().color = Color.white;
                FloatingText.GetComponent<TextMesh> ().text = showLiveInText.ToString ();
            } else {
                FloatingText.GetComponent<TextMesh> ().color = Color.green;
                FloatingText.GetComponent<TextMesh> ().text = "Esquivo";
            }
        } else if (character.stats.takeCoin == false) {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = "Dead!";
        }
        character.stats.takeCoin = false;
    }

    public void Idle () {
        state = states.IDLE;
        character.stats.characterAttack = false;
        animations.idleAnimation ();
    }

    public void Walk (int direction) {
        Vector3 movement = new Vector3 (direction, 0f, 0f);
        transform.position += movement * Time.deltaTime * character.stats.moveSpeed;
        character.stats.characterAttack = false;
        if (direction == 1) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }

        if (groundcheck.onGround == true) {
            animations.walkAnimation ();
            state = states.WALKING;
        } else {
            animations.jumpAnimation ();
            state = states.JUMPING;
        }
    }

    public void Jump () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, character.stats.jumpForce), ForceMode2D.Impulse);
        state = states.JUMPING;
        character.stats.characterAttack = false;
        animations.jumpAnimation ();
    }

    public void Attack () {
        character.stats.characterForce = Random.Range (character.stats.characterMinForce, character.stats.characterMaxForce);
        state = states.ATTACKING;
        character.stats.characterAttack = true;
        animations.attackAnimation ();
    }

    public void Hurt () {
        state = states.HURT;
        ShowFloatingText ();
        character.stats.characterAttack = false;
        animations.hurtAnimation ();
    }

    public void Die () {
        state = states.DEATH;
        character.stats.characterAttack = false;
        animations.dieAnimation ();
    }
}