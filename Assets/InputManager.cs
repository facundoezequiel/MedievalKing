using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Character character;
    public CharacterActions actions;
    public GroundCheck groundcheck;
    public BlackEnemy blackEnemy;
    public GameManager gameManager;
    public bool attackCounter = true;
    public bool pegarButton;
    public bool pressP;

    void Update () {
        // Si el nivel no esta terminado, entonces puede moverse
        if (gameManager.levelComplete == false) {
            // Si el personaje no esta muerto, entonces puede moverse
            if (character.stats.characterDie == false) {
                // Si el super poder no esta activo, entonces puede moverse
                if (character.stats.superPowerActive == false) {
                    float horizontal = (Input.GetAxis ("Horizontal"));
                    bool jumpButton = (Input.GetButtonDown ("Jump"));
                    bool meditarButton = (Input.GetButtonDown ("Meditar"));
                    pegarButton = (Input.GetButtonDown ("Pegar"));
                    bool superataqueButton = (Input.GetButtonDown ("Superataque"));
                    // Caminar hacia la izquierda
                    if (Input.GetKey (KeyCode.A) || horizontal < 0) {
                        actions.Walk (-1);
                    }
                    // Caminar hacia la derecha
                    if (Input.GetKey (KeyCode.D) || horizontal > 0) {
                        actions.Walk (1);
                    }
                    // Salto
                    if (Input.GetKeyDown (KeyCode.W) || jumpButton == true) {
                        if (groundcheck.onGround == true) {
                            actions.Jump ();
                        }
                    }
                    // Ataque
                    if (Input.GetKeyDown (KeyCode.P) || pegarButton == true) {
                        // Si el contador de ataque es true puede pegar
                        if (attackCounter == true) {
                            actions.Attack ();
                            // Desactivo el contador de ataqaue
                            attackCounter = false;
                            pressP = true;
                            // Inovoko un reset en la velocidad de ataque para resetar el contador y que pueda volver a atacar
                            Invoke ("attackCounterReset", character.stats.characterAttackSpeed);
                        } else {
                            pressP = false;
                        }
                    }
                    // Super poder
                    if (Input.GetKeyDown (KeyCode.O) || superataqueButton == true) {
                        actions.SuperPower ();
                    }
                    // Meditar
                    if (Input.GetKeyDown (KeyCode.M) || meditarButton == true) {
                        if (character.stats.characterMeditar == false) {
                            actions.Meditar ();
                        }
                    }
                    // Si aprieta las dos direcciones al mismo tiempo se queda quieto en el medio
                    if (!Input.anyKey || Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) {
                        actions.Idle ();
                    }
                }
            }
            // Si el personaje esta muerto ejecutar la funcion de morir
            else {
                actions.Die ();
            }
        }
    }

    // Resetea el control de ataque 
    private void attackCounterReset () {
        attackCounter = true;
    }
}