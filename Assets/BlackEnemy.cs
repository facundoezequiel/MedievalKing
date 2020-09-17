using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : MonoBehaviour {
    public Character character;
    public Animator anim;
    private Transform target;
    public float blackEnemyLive = 40;
    public float blackEnemyMoveSpeed = 7;
    public bool characterAttackingZone = false;
    public bool characterEnterInRightZone = false;
    public bool characterEnterInLeftZone = false;
    public BlackEnemyFollowRightZone followRightZone;
    public BlackEnemyFollowLeftZone followLeftZone;

    void Start () {
        anim = GetComponent<Animator> ();
        blackEnemyIdle ();
        target = GameObject.FindGameObjectWithTag ("Character").GetComponent<Transform> ();
    }

    void Update () {
        if (blackEnemyLive != 0 && characterEnterInLeftZone == true || characterEnterInRightZone == true) {
            blackEnemyRotation ();
            transform.position = Vector2.MoveTowards (transform.position, target.position, blackEnemyMoveSpeed * Time.deltaTime);
        } else if (blackEnemyLive == 0) {
            anim.SetBool ("isDying", true);
            Invoke ("blackEnemyDie", 1.3f);
            Destroy (GetComponent<BoxCollider2D> ());
        }
    }

    public void blackEnemyRotation () {
        if (characterEnterInLeftZone == true) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else if (characterEnterInRightZone == true) {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }
    }

    public void blackEnemyIdle () {
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isHurt", false);
    }

    public void blackEnemyHurt () {
        blackEnemyLive = blackEnemyLive - 10;
        anim.SetBool ("isHurt", true);
        Invoke ("blackEnemyIdle", 0.3f);
    }

    public void blackEnemyDie () {
        Destroy (this.gameObject);
    }
}