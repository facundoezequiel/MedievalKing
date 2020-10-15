using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour {
    public Animator HeartAnim;
    public Animator ManaAnim;
    public AudioSource coinSound;
    public Character character;
    public SuperPowerArea SuperPowerArea;
    public GameObject FloatingTextPrefab;
    public GameObject ElectricSuperPowerPrefab;
    public GameObject ElectricSuperPowerPrefab2;
    public GameObject MeditarParticlesPrefab;
    public CharacterAnimations animations;
    public Text CoinsText;
    public Text HeartText;
    public Text ManaText;
    public float showLiveInText;
    public float showFireForceInText;
    public GroundCheck groundcheck;
    public states state;
    public enum states {
        IDLE,
        WALKING,
        JUMPING,
        HURT,
        ATTACKING,
        DEATH,
        SUPERPOWER,
        MEDITAR
    }

    public void Start () {
        HeartText.text = character.stats.characterLive.ToString ();
        ManaText.text = character.stats.characterMana.ToString ();
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
        }
        if (state == states.DEATH) {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = "Dead!";
        }
        if (state == states.MEDITAR) {
            FloatingText.GetComponent<TextMesh> ().color = Color.cyan;
            FloatingText.GetComponent<TextMesh> ().text = "+1";
        }
        if (state == states.SUPERPOWER) {
            FloatingText.GetComponent<TextMesh> ().color = Color.cyan;
            FloatingText.GetComponent<TextMesh> ().text = "-80";
        }
        if (character.stats.characterOnFire == true && character.stats.characterDie == false) {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = showFireForceInText.ToString ();
        }
        character.stats.takeCoin = false;
    }

    public void Idle () {
        state = states.IDLE;
        character.stats.superPowerActive = false;
        character.stats.characterAttack = false;
        animations.idleAnimation ();
    }

    public void Walk (int direction) {
        Vector3 movement = new Vector3 (direction, 0f, 0f);
        transform.position += movement * Time.deltaTime * character.stats.moveSpeed;
        character.stats.superPowerActive = false;
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
        character.stats.superPowerActive = false;
        character.stats.characterAttack = false;
        animations.jumpAnimation ();
    }

    public void Attack () {
        if (character.stats.characterDie == false) {
            character.stats.superPowerActive = false;
            character.stats.characterForce = Random.Range (character.stats.characterMinForce, character.stats.characterMaxForce);
            state = states.ATTACKING;
            character.stats.characterAttack = true;
            animations.attackAnimation ();
        } else {
            return;
        }
    }

    public void SuperPower () {
        if (character.stats.characterMana >= 80) {
            state = states.SUPERPOWER;
            character.stats.superPowerActive = true;
            animations.superPowerAnimation ();
            SuperPowerArea.playAnimation ();
            var ElectricSuperPower = Instantiate (ElectricSuperPowerPrefab, transform.position, Quaternion.identity, transform);
            var ElectricSuperPower2 = Instantiate (ElectricSuperPowerPrefab2, transform.position, Quaternion.identity, transform);
            character.stats.characterMana = character.stats.characterMana - 80;
            ShowFloatingText ();
            ManaText.text = character.stats.characterMana.ToString ();
            ManaAnim.Play ("Base Layer.ManaImage", 0, 0.25f);
        } else {
            character.stats.superPowerActive = false;
        }
    }

    public void Meditar () {
        if (state == states.IDLE) {
            state = states.MEDITAR;
        }
        if (character.stats.characterMana < character.stats.characterMaxMana && state == states.MEDITAR) {
            character.stats.characterMeditar = true;
            character.stats.characterMana = character.stats.characterMana + 1;
            character.stats.superPowerActive = false;
            var MeditarParticles = Instantiate (MeditarParticlesPrefab, transform.position, Quaternion.identity, transform);
            ManaText.text = character.stats.characterMana.ToString ();
            ManaAnim.Play ("Base Layer.ManaImage", 0, 0.25f);
            ShowFloatingText ();
            Invoke ("Meditar", 0.2f);
        } else {
            character.stats.characterMeditar = false;
        }
    }

    public void Hurt () {
        state = states.HURT;
        character.stats.superPowerActive = false;
        ShowFloatingText ();
        HeartText.text = character.stats.characterLive.ToString ();
        HeartAnim.Play ("Base Layer.HeartImage", 0, 0.25f);
        character.stats.characterAttack = false;
        animations.hurtAnimation ();
    }

    public void Die () {
        state = states.DEATH;
        character.stats.superPowerActive = false;
        character.stats.characterAttack = false;
        HeartText.text = "Dead!";
        animations.dieAnimation ();
    }
}