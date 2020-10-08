using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackEnemy : MonoBehaviour {
    public Character character;
    public Animator anim;
    private Transform target;
    public GameObject FloatingTextPrefab;
    public float blackEnemyLive = 40;
    public float blackEnemyMoveSpeed = 7;
    public int blackEnemyMinForce = 2;
    public int blackEnemyMaxForce = 7;
    public int blackEnemyForce = 5;
    public bool floatingTextActive = false;
    public bool enemyRecover = false;
    public bool characterAttackingZone = false;
    public bool characterEnterInRightZone = false;
    public bool characterEnterInLeftZone = false;
    public BlackEnemyFollowRightZone followRightZone;
    public BlackEnemyFollowLeftZone followLeftZone;

    void Start () {
        anim = GetComponent<Animator> ();
        target = GameObject.FindGameObjectWithTag ("Character").GetComponent<Transform> ();
    }

    void Update () {
        if (blackEnemyLive > 0) {
            if (characterEnterInLeftZone == true || characterEnterInRightZone == true) {
                BlackEnemyRotation ();
                if (characterAttackingZone == false) {
                    BlackEnemyWalk ();
                } else {
                    if (Input.GetKeyDown (KeyCode.P)) {
                        BlackEnemyHurt ();
                    } else if (enemyRecover == false) {
                        BlackEnemyAttack ();
                    }
                }
            } else {
                BlackEnemyIdle ();
            }
        } else {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking", false);
            anim.SetBool ("isRecover", false);
            anim.SetBool ("isDying", true);
            if (floatingTextActive == false) {
                ShowFloatingText ();
            }
            Invoke ("BlackEnemyBeforeDie", 1f);
        }
    }

    public void ShowFloatingText () {
        var FloatingText = Instantiate (FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        floatingTextActive = true;
        if (blackEnemyLive > 0) {
            if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
                FloatingText.GetComponent<TextMesh> ().color = Color.red;
                FloatingText.GetComponent<TextMesh> ().text = "Stuned! " + character.stats.characterForce.ToString ();;
            } else {
                FloatingText.GetComponent<TextMesh> ().color = Color.white;
                FloatingText.GetComponent<TextMesh> ().text = character.stats.characterForce.ToString ();
            }
        } else {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = "Dead!";
        }
    }

    public void BlackEnemyIdle () {
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("isRecover", false);
        floatingTextActive = false;
    }

    public void BlackEnemyWalk () {
        if (enemyRecover == false) {
            anim.SetBool ("isDying", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking", false);
            anim.SetBool ("isRecover", false);
            anim.SetBool ("isWalking", true);
            transform.position = Vector2.MoveTowards (transform.position, target.position, blackEnemyMoveSpeed * Time.deltaTime);
            floatingTextActive = false;
        }
    }

    public void BlackEnemyAttack () {
        if (character.stats.characterDie == true) {
            BlackEnemyIdle ();
        }
        var acurrenceAttack = Random.Range (0, 70);
        if (character.stats.characterDie == false && acurrenceAttack == 69) {
            blackEnemyForce = Random.Range (blackEnemyMinForce, blackEnemyMaxForce);
            character.stats.characterLive = character.stats.characterLive - blackEnemyForce;
            character.actions.showLiveInText = blackEnemyForce;
            character.actions.Hurt ();
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isDying", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isRecover", false);
            anim.SetBool ("isAttacking", true);
        } else if (character.stats.characterDie == false && acurrenceAttack != 69) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isDying", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isRecover", false);
            anim.SetBool ("isAttacking", true);
            character.actions.showLiveInText = 0;
        }
        floatingTextActive = false;
    }

    public void BlackEnemyHurt () {
        blackEnemyLive = blackEnemyLive - character.stats.characterForce;
        if (character.stats.characterForce < character.stats.characterMaxForce - 1) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isDying", false);
            anim.SetBool ("isAttacking", false);
            anim.SetBool ("isRecover", false);
            anim.SetBool ("isHurt", true);
            enemyRecover = false;
        } else if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isDying", false);
            anim.SetBool ("isAttacking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isRecover", true);
            enemyRecover = true;
            Invoke ("EnemyGettingUp", 0.7f);
        }
        ShowFloatingText ();
    }

    public void EnemyGettingUp () {
        enemyRecover = false;
    }

    public void BlackEnemyRotation () {
        if (characterEnterInLeftZone == true) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else if (characterEnterInRightZone == true) {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }
    }

    public void BlackEnemyBeforeDie () {
        Destroy (GetComponent<BoxCollider2D> ());
        Invoke ("BlackEnemyDie", 1f);
    }

    public void BlackEnemyDie () {
        characterAttackingZone = false;
        Destroy (this.gameObject);
    }
}