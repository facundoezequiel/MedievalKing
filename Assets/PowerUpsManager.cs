using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour {
    public Character character;
    public Fire fire;
    public BlackEnemy blackenemy;
    public GameManager gameManager;
    public bool extra50Live = false;
    public bool extra3Force = false;
    public bool extra50Mana = false;

    // Autonivelacion de dificultad
    public void ActivarPowerUps () {
        // +50 de vida a personaje
        if (extra50Live == true) {
            character.stats.characterMaxLive += 50;
            fire.FireForceMin += 15;
            fire.FireForceMax += 15;
            blackenemy.blackEnemyLive += 30;
            if (character.stats.characterAttackSpeed >= 0.1f) {
                character.stats.characterAttackSpeed -= 0.05f;
            }
            if (character.stats.characterLive < character.stats.characterMaxLive) {
                character.stats.characterLive = character.stats.characterMaxLive;
                character.stats.HeartAnim.Play ("Base Layer.HeartImage", 0, 0.25f);
                character.stats.HeartText.text = character.stats.characterLive.ToString ();
            }
            character.actions.ShowFloatingText ();
            extra50Live = false;
            gameManager.puntaje = gameManager.puntaje + 50000;
        }
        // +3 de fuerza a personaje
        if (extra3Force == true) {
            character.stats.characterMinForce += 3;
            character.stats.characterMaxForce += 3;
            character.stats.characterForce += 3;
            blackenemy.blackEnemyMinForce += 5;
            blackenemy.blackEnemyMaxForce += 5;
            blackenemy.blackEnemyForce += 5;
            character.actions.ShowFloatingText ();
            extra3Force = false;
            gameManager.puntaje = gameManager.puntaje + 50000;
        }
        // +50 de mana a personaje
        if (extra50Mana == true) {
            character.stats.characterMaxMana = character.stats.characterMaxMana + 50;
            character.stats.superPowerManaCost = character.stats.superPowerManaCost + 50;
            character.stats.characterManaReload = character.stats.characterManaReload + 1;
            if (character.stats.characterMana < character.stats.characterMaxMana) {
                character.stats.characterMana = character.stats.characterMaxMana;
                character.actions.ManaText.text = character.stats.characterMana.ToString ();
                character.actions.ManaAnim.Play ("Base Layer.ManaImage", 0, 0.25f);
            }
            character.actions.ShowFloatingText ();
            extra50Mana = false;
            gameManager.puntaje = gameManager.puntaje + 50000;
        }
    }
}