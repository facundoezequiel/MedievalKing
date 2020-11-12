using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour {
    public Character character;
    public Animator anim;
    private Transform target;
    public GameObject FloatingTextPrefab;
    public float positionY;
    public float finalBossLive = 500;
    public float finalBossChestLive = 50;
    public float finalBossMoveSpeed = 5;
    public int finalBossMinForce = 20;
    public int finalBossMaxForce = 30;
    public int finalBossForce = 29;
    public int randomAttack;
    public int superAttackRandom;
    public int randomHurt;
    public bool finalBossSuperAttack = false;
    public bool floatingTextActive = false;
    public bool finalBossRecover = false;
    public bool finalBossAttackingZone = false;
    public bool characterAttackingZone = false;
    public bool characterEnterInRightZone = false;
    public bool characterEnterInLeftZone = false;
    public FinalBossFollowRightZone finalBossFollowRightZone;
    public FinalBossFollowLeftZone finalBossFollowLeftZone;
    public GameObject FinalBossParticlesRealodSuperPowerPrefab;
    public GameObject FinalBossParticlesSuperPowerPrefab;

    void Start () {
        anim = GetComponent<Animator> ();
        target = GameObject.FindGameObjectWithTag ("Character").GetComponent<Transform> ();
        randomAttack = Random.Range (1, 4);
        randomHurt = Random.Range (0, 5);
        positionY = transform.position.y;
    }

    void Update () {
        if (finalBossLive > 0) {
            if (finalBossChestLive == 50) {
                print ("Boss final activo");
                if (characterEnterInLeftZone == true || characterEnterInRightZone == true) {
                    FinalBossRotation ();
                    if (finalBossAttackingZone == false) {
                        FinalBossWalk ();
                    } else {
                        if (Input.GetKeyDown (KeyCode.P) && character.input.pressP == true && characterAttackingZone == true) {
                            randomHurt = Random.Range (0, 5);
                            if (randomHurt != 4) {
                                FinalBossHurt ();
                            } else {
                                FinalBossBlock ();
                            }
                        } else if (finalBossRecover == false) {
                            FinalBossAttack ();
                        }
                    }
                } else {
                    FinalBossIdle ();
                }
            }
        } else {
            print ("Boss final muerto");
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", true);
            if (floatingTextActive == false) {
                ShowFloatingText ();
            }
            Invoke ("FinalBossBeforeDie", 2f);
        }
    }

    public void ShowFloatingText () {
        var FloatingText = Instantiate (FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        floatingTextActive = true;
        if (finalBossLive > 0) {
            if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
                FloatingText.GetComponent<TextMesh> ().color = Color.red;
                FloatingText.GetComponent<TextMesh> ().text = "Stuned! " + character.stats.characterForce.ToString ();;
            } else {
                FloatingText.GetComponent<TextMesh> ().color = Color.blue;
                FloatingText.GetComponent<TextMesh> ().text = "-" + character.stats.characterForce.ToString ();
            }
            if (randomHurt == 4) {
                FloatingText.GetComponent<TextMesh> ().color = Color.cyan;
                FloatingText.GetComponent<TextMesh> ().text = "Block!";
            }
        } else {
            FloatingText.GetComponent<TextMesh> ().color = Color.red;
            FloatingText.GetComponent<TextMesh> ().text = "Dead!";
        }
    }

    public void FinalBossIdle () {
        if (finalBossSuperAttack == false) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            floatingTextActive = false;
        }
    }

    public void FinalBossWalk () {
        if (finalBossSuperAttack == false) {
            transform.position = new Vector3 (transform.position.x, positionY, transform.position.z);
            if (finalBossRecover == false) {
                anim.SetBool ("isWalking", true);
                anim.SetBool ("isHurt", false);
                anim.SetBool ("isAttacking1", false);
                anim.SetBool ("isAttacking2", false);
                anim.SetBool ("isAttacking3", false);
                anim.SetBool ("isBlock", false);
                anim.SetBool ("isDying", false);
                transform.position = Vector2.MoveTowards (transform.position, target.position, finalBossMoveSpeed * Time.deltaTime);
                floatingTextActive = false;
            }
        }
    }

    public void FinalBossAttack () {
        if (character.stats.characterDie == true) {
            FinalBossIdle ();
        }
        var acurrenceAttack = Random.Range (0, 70);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", false);
        if (character.stats.characterDie == false && acurrenceAttack == 69 && finalBossSuperAttack == false) {
            randomAttack = Random.Range (1, 4);
            if (randomAttack == 1 || randomAttack == 2) {
                finalBossForce = Random.Range (finalBossMinForce, finalBossMaxForce);
                character.stats.characterLive = character.stats.characterLive - finalBossForce;
                character.actions.showLiveInText = finalBossForce;
                character.actions.Hurt ();
                anim.SetBool ("isWalking", false);
                anim.SetBool ("isHurt", false);
                anim.SetBool ("isBlock", false);
                anim.SetBool ("isDying", false);
                if (randomAttack == 1) {
                    anim.SetBool ("isAttacking2", false);
                    anim.SetBool ("isAttacking3", false);
                    anim.SetBool ("isAttacking1", true);
                } else if (randomAttack == 2) {
                    anim.SetBool ("isAttacking1", false);
                    anim.SetBool ("isAttacking3", false);
                    anim.SetBool ("isAttacking2", true);
                }
            }
            if (randomAttack == 3) {
                superAttackRandom = Random.Range (0, 80);
                if (superAttackRandom > 70) {
                    finalBossSuperAttack = true;
                    var FinalBossParticlesRealodSuperPower = Instantiate (FinalBossParticlesRealodSuperPowerPrefab, transform.position, Quaternion.identity, transform);
                    anim.SetBool ("isWalking", false);
                    anim.SetBool ("isHurt", false);
                    anim.SetBool ("isAttacking1", false);
                    anim.SetBool ("isAttacking2", false);
                    anim.SetBool ("isAttacking3", false);
                    anim.SetBool ("isBlock", true);
                    anim.SetBool ("isDying", false);
                    Invoke ("FinalBossSuperAttack", 2f);
                } else {
                    randomAttack = Random.Range (1, 3);
                    finalBossForce = Random.Range (finalBossMinForce, finalBossMaxForce);
                    character.stats.characterLive = character.stats.characterLive - finalBossForce;
                    character.actions.showLiveInText = finalBossForce;
                    character.actions.Hurt ();
                    anim.SetBool ("isWalking", false);
                    anim.SetBool ("isHurt", false);
                    anim.SetBool ("isBlock", false);
                    anim.SetBool ("isDying", false);
                    if (randomAttack == 1) {
                        anim.SetBool ("isAttacking2", false);
                        anim.SetBool ("isAttacking3", false);
                        anim.SetBool ("isAttacking1", true);
                    } else if (randomAttack == 2) {
                        anim.SetBool ("isAttacking1", false);
                        anim.SetBool ("isAttacking3", false);
                        anim.SetBool ("isAttacking2", true);
                    }
                }
            }
        } else if (character.stats.characterDie == false && acurrenceAttack != 69) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            randomAttack = Random.Range (1, 3);
            if (randomAttack == 1) {
                anim.SetBool ("isAttacking1", true);
            } else if (randomAttack == 2) {
                anim.SetBool ("isAttacking2", true);
            }
            character.actions.showLiveInText = 0;
        }
        floatingTextActive = false;
    }

    // Function Super Attack - Cuando sale el super ataque en el random 
    public void FinalBossSuperAttack () {
        finalBossForce = Random.Range (finalBossMinForce, finalBossMaxForce);
        character.stats.characterLive = character.stats.characterLive - (finalBossForce + 80);
        character.actions.showLiveInText = finalBossForce;
        character.actions.Hurt ();
        var FinalBossParticlesSuperPower = Instantiate (FinalBossParticlesSuperPowerPrefab, transform.position, Quaternion.identity, transform);
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isBlock", false);
        anim.SetBool ("isDying", false);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", true);
        finalBossSuperAttack = false;
    }

    // Function Block - Cuando bloquea un ataque
    public void FinalBossBlock () {
        if (finalBossSuperAttack == false) {
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", true);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", true);
            anim.SetBool ("isDying", false);
            ShowFloatingText ();
        }
    }

    // Function Hurt - Cuando recibe un ataque
    public void FinalBossHurt () {
        if (finalBossSuperAttack == false) {
            finalBossLive = finalBossLive - character.stats.characterForce;
            if (character.stats.characterForce < character.stats.characterMaxForce - 1) {
                anim.SetBool ("isWalking", false);
                anim.SetBool ("isHurt", true);
                anim.SetBool ("isAttacking1", false);
                anim.SetBool ("isAttacking2", false);
                anim.SetBool ("isAttacking3", false);
                anim.SetBool ("isBlock", false);
                anim.SetBool ("isDying", false);
                finalBossRecover = false;
            } else if (character.stats.characterForce == character.stats.characterMaxForce - 1) {
                anim.SetBool ("isWalking", false);
                anim.SetBool ("isHurt", true);
                anim.SetBool ("isAttacking1", false);
                anim.SetBool ("isAttacking2", false);
                anim.SetBool ("isAttacking3", false);
                anim.SetBool ("isBlock", false);
                anim.SetBool ("isDying", false);
                finalBossRecover = true;
                Invoke ("FinalBossGettingUp", 0.7f);
            }
            ShowFloatingText ();
        }
    }

    public void FinalBossGettingUp () {
        finalBossRecover = false;
    }

    public void FinalBossRotation () {
        if (characterEnterInLeftZone == true) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        } else if (characterEnterInRightZone == true) {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        }
    }

    public void FinalBossBeforeDie () {
        Destroy (GetComponent<BoxCollider2D> ());
        Invoke ("FinalBossDie", 1f);
    }

    public void FinalBossDie () {
        finalBossAttackingZone = false;
        characterAttackingZone = false;
        Destroy (this.gameObject);
    }
}