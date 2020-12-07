using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaDePuntaje : MonoBehaviour {
    public Transform container;
    public Transform template;
    public List<PuntajeEntrada> puntajeEntradaList;
    public List<Transform> puntajeEntradaTransformList;

    // BUSCAR ESTO
    private void Awake () {
        // Busca a estos objectos por su nombre
        container = transform.Find ("PuntajeContainer");
        template = container.Find ("PuntajeTemplate");
        // Apago el template de puntaje
        template.gameObject.SetActive (false);
        // BUSCAR ESTO
        puntajeEntradaList = new List<PuntajeEntrada>() {
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
            new PuntajeEntrada{ score = 521, name = "AAA"},
        };

        // BUSCAR ESTO
        puntajeEntradaTransformList = new List<Transform>();
        foreach (PuntajeEntrada puntajeEntrada in puntajeEntradaList) {
            CrearEntradaPuntaje(puntajeEntrada, container, puntajeEntradaTransformList);
        }
    }

    private void CrearEntradaPuntaje (PuntajeEntrada puntajeEntrada, Transform container, List<Transform> transformList) {
        // Establezco la diferencia de altura entre los puntajes
        float puntajeAltura = 30f;
        // BUSCAR ESTO
        Transform puntajeTransform = Instantiate (template, container);
        // BUSCAR ESTO
        RectTransform puntajeRectTransform = puntajeTransform.GetComponent<RectTransform> ();
        // Posiciono el nuevo puntaje con una diferencia de altura en Y sobre el ultimo
        puntajeRectTransform.anchoredPosition = new Vector2 (0, -puntajeAltura * transformList.Count);
        // Muestro el nuevo puntaje
        puntajeTransform.gameObject.SetActive (true);
        // Establezco la ranking de los puntajes mediante el for
        int ranking = transformList.Count + 1;
        // Creo un string donde se va mostrar la posicion del ranking para el puntaje
        string rankingString;
        // Hago un switch el cual me va a permitir hacer diferentes cosas depende el ranking
        switch (ranking) {
            // En casa caso, muestro el texto correspondiente en el string del ranking
            // Si ranking no es 1, 2 o 3 entra el default
            // BUSCAR BREAK
            default : rankingString = ranking + "TH";
            break;
            // Si llega a ser 1, 2 o 3 (Esto es solo para poner correctamente la numeracion)
            case 1:
                    rankingString = "1ST";
                break;
            case 2:
                    rankingString = "2ST";
                break;
            case 3:
                    rankingString = "3ST";
                break;
        }
        // Busco el texto de posicion y muestro el string del ranking
        puntajeTransform.Find ("PosText").GetComponent<Text> ().text = rankingString;
        // Random de puntaje CAMBIAR
        int score = puntajeEntrada.score;
        // Busco el texto de puntaje y muestro el puntaje 
        puntajeTransform.Find ("PuntajeText").GetComponent<Text> ().text = score.ToString ();
        // Seteo el nombre CAMBIAR
        string name = puntajeEntrada.name;
        // Busco el texto de nombre y muestro el nombre
        puntajeTransform.Find ("NameText").GetComponent<Text> ().text = name;
        // BUSCAR ESTO
        transformList.Add(puntajeTransform);
    }

    // Representa una sola entrada de highscore
    public class PuntajeEntrada {
        public int score;
        public string name;
    }
}