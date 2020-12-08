using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTransfer : MonoBehaviour {
    public GameObject inputField;
    public GameObject textDisplay;
    public GameManager gameManager;
    public Text TextoContador;
    public int contador;
    public bool inputCompletado;

    public void Start () {
        // Al inciar desactivo el texto del contador
        TextoContador.gameObject.SetActive (false);
        // Pongo el contador en 3
        contador = 3;
        // El input no esta completado
        inputCompletado = false;
    }

    // Funcion que se activa al hacer click en ingresar
    public void InsertarName () {
        // Guardo el nombre ingresado en la varibale de playerName de GameManager, que se usara para el puntaje
        gameManager.playerName = inputField.GetComponent<Text> ().text;
        // Cambio el texto que pedia el nombre por un saludo de bienvenida
        textDisplay.GetComponent<Text> ().text = "Bienvenidx, " + gameManager.playerName + " a Medieval King";
        // Muestro el texto del contador
        TextoContador.gameObject.SetActive (true);
        // Llamo a la funcion que inicia la contador
        IniciarContador ();
    }

    // Funcion que activa la boleano de input completado
    public void InputIngresado () {
        // El input se completo
        inputCompletado = true;
    }

    // Funcion que inicia el contador
    public void IniciarContador () {
        // Muestra el texto del contador con el contador actualizado cada vez que la llaman
        TextoContador.text = "El juego comienza en " + contador.ToString () + "...";
        // Lllama en un segundo una funcion que le resta al contador
        Invoke ("RestarContador", 1);
    }

    // Funcion que le resta al contador
    public void RestarContador () {
        // Si el contador llega a 0
        if (contador == 0) {
            // Llamo a la funcion que pone el buleano de input completado en true
            InputIngresado ();
        } 
        // Si el contador no es 0
        else {
            // Restarle 1 al contador
            contador--;
            // Vuevle a llamar a la funcion que inicia el contador y actualiza el texto para generar el loop
            IniciarContador ();
        }
    }
}