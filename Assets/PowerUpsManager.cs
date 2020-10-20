using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour {
    public Character character;
    public bool extra50Live = false;
    public bool extra3Force = false;
    public bool extra50Mana = false;

    void Update () {
        ActivarPowerUps ();
    }

    public void ActivarPowerUps () {
        // +50 de vida
        if (extra50Live == true) {
            character.stats.characterMaxLive = character.stats.characterMaxLive + 50;
            if (character.stats.characterLive < character.stats.characterMaxLive) {
                character.stats.characterLive = character.stats.characterMaxLive;
            }
            extra50Live = false;
        }
        // +3 de fuerza
        if (extra3Force == true) {
            character.stats.characterMinForce = character.stats.characterMinForce + 3;
            character.stats.characterMaxForce = character.stats.characterMaxForce + 3;
            character.stats.characterForce = character.stats.characterForce + 3;
            extra3Force = false;
        }
        // +50 de mana 
        if (extra50Mana == true) {
            character.stats.characterMaxMana = character.stats.characterMaxMana + 50;
            if (character.stats.characterMana < character.stats.characterMaxMana) {
                character.stats.characterMana = character.stats.characterMaxMana;
            }
            extra50Mana = false;
        }
    }
}