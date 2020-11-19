using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour {
    public Character character;
    public GameObject escapePoint;
    public Animator anim;
    private Transform target;
    public GameObject FloatingTextPrefab;
    public GameManager gameManager;
    public float positionY;
    public float finalBossLive = 300;
    public float finalBossEscapeLive = 50;
    public float finalBossMoveSpeed = 5;
    public int finalBossMinForce = 10;
    public int finalBossMaxForce = 17;
    public int finalBossForce = 16;
    public int randomAttack;
    public int superAttackRandom;
    public int randomHurt;
    public bool finalBossSuperAttack = false;
    public bool floatingTextActive = false;
    public bool finalBossAttackingZone = false;
    public bool characterAttackingZone = false;
    public bool characterEnterInRightZone = false;
    public bool characterEnterInLeftZone = false;
    public bool bossEscape;
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
        bossEscape = false;
    }

    void Update () {
        // Si el juego no esta terminado y si la vida del boss no es cero
        if (gameManager.levelComplete == false && finalBossLive > 0) {
            // Si la vida del boss es mayor a la vida de escape
            if (finalBossLive > finalBossEscapeLive) {
                // Si el jugador entra por alguno de los dos lados
                if (characterEnterInLeftZone == true || characterEnterInRightZone == true) {
                    // Si el boss no esta por hacer el super ataque
                    if (finalBossSuperAttack == false) {
                        // Rota para verlo de frente
                        FinalBossRotation ();
                        // Si el jugador no esta en la zona de ataque
                        if (finalBossAttackingZone == false) {
                            // Camina hacia el jugador
                            FinalBossWalk ();

                        }
                        // Si el jugador esta en zona de ataque
                        else {
                            // Si aprieta el boton de pegar le pega
                            if (Input.GetKeyDown (KeyCode.P) && character.input.pressP == true && characterAttackingZone == true) {
                                // Random de ataque del jugador
                                randomHurt = Random.Range (0, 5);
                                // Si es distinto a 4
                                if (randomHurt != 4) {
                                    // Le hace daño
                                    FinalBossHurt ();
                                }
                                // Si es 4
                                else {
                                    // El boss bloquea el ataque
                                    FinalBossBlock ();
                                }
                            }
                            // Si no lo atacan
                            else {
                                // El boss ataca al jugador
                                FinalBossAttack ();
                            }
                        }
                    }
                }
                // Si el jugador no entra por ningun lado
                else {
                    // Se queda en IDLE
                    FinalBossIdle ();
                }
            }
            // Si la vida del boss es menor a la vida de escape
            else if (finalBossLive <= finalBossEscapeLive) {
                // El boss escapa
                if (bossEscape == false) {
                    FinalBossEscape ();
                }
                FinalBossWalk ();
            }
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

    // Funcion de ataque
    public void FinalBossAttack () {
        // Si el personaje se murio, no hace nada
        if (character.stats.characterDie == true) {
            FinalBossIdle ();
        }
        // Random de la precision del ataque 
        var acurrenceAttack = Random.Range (0, 70);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", false);
        // Si el personaje no esta muerto y sale 69 en el random y tampoco esta haciendo el super ataque, le pega
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
                if (superAttackRandom > 50) {
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

    // Function del super ataque
    public void FinalBossSuperAttack () {
        // Hace una fuerza dentro de su rango
        finalBossForce = Random.Range (finalBossMinForce, finalBossMaxForce);
        // Al daño que le hace al personaje le sumo 80
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

    // Funcion con la que bloquea un ataque
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

    // Funcion de herida, cunado recibe un ataque
    public void FinalBossHurt () {
        // Si no esta haciendo el super ataque
        if (finalBossSuperAttack == false) {
            finalBossLive = finalBossLive - character.stats.characterForce;
            anim.SetBool ("isWalking", false);
            anim.SetBool ("isHurt", true);
            anim.SetBool ("isAttacking1", false);
            anim.SetBool ("isAttacking2", false);
            anim.SetBool ("isAttacking3", false);
            anim.SetBool ("isBlock", false);
            anim.SetBool ("isDying", false);
            ShowFloatingText ();
        }
    }

    // Control de rotacion dependiendo de donde esta el personje a traves de detecciones
    public void FinalBossRotation () {
        if (characterEnterInLeftZone == true) {
            transform.localRotation = Quaternion.Euler (0, 180, 0);
        } else if (characterEnterInRightZone == true) {
            transform.localRotation = Quaternion.Euler (0, 0, 0);
        }
    }

    // Funcion de escape final cuando el jugador completa el nivel
    public void FinalBossEscape () {
        bossEscape = true;
        finalBossSuperAttack = false;
        characterAttackingZone = false;
        transform.localRotation = Quaternion.Euler (0, 0, 0);
        target = escapePoint.GetComponent<Transform> ();
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isHurt", false);
        anim.SetBool ("isAttacking1", false);
        anim.SetBool ("isAttacking2", false);
        anim.SetBool ("isAttacking3", false);
        anim.SetBool ("isBlock", false);
        anim.SetBool ("isDying", false);
    }
}