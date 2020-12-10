using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaDePuntaje : MonoBehaviour {
    public Transform container;
    public Transform template;
    public List<Transform> puntajeEntradaTransformList;
    public GameManager gameManager;

    /* 
    -------------- COMO FUNCIONA ---------------
    Hay un contenedor que contiene un template de como son todas las entradas de puntaje, ese template contiene la pos,
    el nombre y el puntaje. La idea es duplicar el template varias veces y cada una de las veces que se duplique contenga
    una entrada de puntaje.
    */

    public void Start () {
        // Hago las referencias para traer el contenedor y el tempalte por el nombre
        container = transform.Find ("PuntajeContainer");
        template = container.Find ("PuntajeTemplate");
        // Apago el template para que no se vea la plantilla
        template.gameObject.SetActive (false);
        // Funcion que agrega una entrada a la lista cuando termina la partida, le paso el puntaje y el nombre desde el GameManager
        AddPuntajeEntrada (gameManager.puntaje, gameManager.playerName.ToString ());
        // Trae la lista de puntajes del json y lo guarda en un string
        string jsonString = PlayerPrefs.GetString ("TablaDePuntaje");
        // Lo guarda en una lista
        Puntajes puntajes = JsonUtility.FromJson<Puntajes> (jsonString);

        // Ordenar las entradas por puntaje maximo
        for (int i = 0; i < puntajes.puntajeEntradaList.Count; i++) {
            for (int j = i + 1; j < puntajes.puntajeEntradaList.Count; j++) {
                if (puntajes.puntajeEntradaList[j].score > puntajes.puntajeEntradaList[i].score) {
                    PuntajeEntrada tmp = puntajes.puntajeEntradaList[i];
                    puntajes.puntajeEntradaList[i] = puntajes.puntajeEntradaList[j];
                    puntajes.puntajeEntradaList[j] = tmp;
                }
            }
        }

        // Crea la lista
        puntajeEntradaTransformList = new List<Transform> ();
        // Por cada elmento de la lista ejecuta la funcion para crear la entrada
        foreach (PuntajeEntrada puntajeEntrada in puntajes.puntajeEntradaList) {
            // Funcion que crea las entradas
            CrearEntradaPuntaje (puntajeEntrada, container, puntajeEntradaTransformList);
        }
    }

    // Funcion que crea las entradas
    // Recibe una entrada de puntaje, el transform del container y una lista de transfroms
    public void CrearEntradaPuntaje (PuntajeEntrada puntajeEntrada, Transform container, List<Transform> transformList) {
        // Muestre hasta la posicion 5 de la lista (el count es como un for pero de lista)
        if ((transformList.Count + 1) <= 5) {
            // Establezco la diferencia de altura entre los templates
            float puntajeAltura = 45f;
            // Pone un nuevo template dentro del container
            Transform puntajeTransform = Instantiate (template, container);
            // Gaurdo el RectTransform del template creado
            RectTransform puntajeRectTransform = puntajeTransform.GetComponent<RectTransform> ();
            // Posiciono el nuevo template en base al anclaje tomando en cuenta la altura y multiplicandola por el count
            // De esta forma se van a posicionar cada template una abajo del otro con la distancia que le aplique de altura
            puntajeRectTransform.anchoredPosition = new Vector2 (0, -puntajeAltura * transformList.Count);
            // Activo el nuevo template para que se vea
            puntajeTransform.gameObject.SetActive (true);
            // Establezco una varibale ranking en base a la posicion del template en la lista
            int ranking = transformList.Count + 1;
            // Creo un string donde se va mostrar la posicion del ranking para el nuevo template
            string rankingString;
            // Hago un switch el cual me va a permitir hacer diferentes cosas depende el ranking
            switch (ranking) {
                // En casa caso, muestro el texto correspondiente en el string del ranking
                // Si ranking no es 1, 2 o 3 entra el default
                default : rankingString = ranking + "TH";
                // Sale del switch
                break;
                // Si llega a ser 1, 2 o 3 (Esto es solo para poner correctamente la numeracion ya sea en ingles o español)
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

            // Funcion que muestra el puntaje y lo agrega a la lista
            AgregarPuntaje ();

            // Funcion que agrega el puntaje a la lista y tambien le asigna a cada texto lo que le corresponde
            void AgregarPuntaje () {
                // Busco el texto de posicion y muestro el string del ranking
                puntajeTransform.Find ("PosText").GetComponent<Text> ().text = rankingString;
                // Crea la varible score y toma de la clase de puntajeEntrada
                int score = puntajeEntrada.score;
                // Busco el texto de puntaje y muestro el puntaje 
                puntajeTransform.Find ("PuntajeText").GetComponent<Text> ().text = score.ToString ();
                // Creo la varibale nombre y toma de la clase de puntajeEntrada
                string name = puntajeEntrada.name;
                // Busco el texto de nombre y muestro el nombre
                puntajeTransform.Find ("NameText").GetComponent<Text> ().text = name;
                // Agrega el nuevo template a la lista con .Add
                transformList.Add (puntajeTransform);
            }
        }
    }

    // Funcion que agrega una entrada de puntaje, es decir, un puntaje nuevo
    public void AddPuntajeEntrada (int score, string name) {
        // Crea un puntaje
        PuntajeEntrada puntajeEntrada = new PuntajeEntrada { score = score, name = name };
        // Guarda en una variable string el string que contiene los puntajes
        string jsonString = PlayerPrefs.GetString ("TablaDePuntaje");
        Puntajes puntajes = JsonUtility.FromJson<Puntajes> (jsonString);
        // Agrega una nueva entrada
        puntajes.puntajeEntradaList.Add (puntajeEntrada);
        // En una varibale string guarda los puntajes en forma de json
        string json = JsonUtility.ToJson (puntajes);
        PlayerPrefs.SetString ("TablaDePuntaje", json);
        // Guarda
        PlayerPrefs.Save ();
    }

    // Esta es la lista donde se jugardan todos las entradas de puntajes
    public class Puntajes {
        public List<PuntajeEntrada> puntajeEntradaList;
    }

    // Permite que se pueda guarda el JSON
    [System.Serializable]

    // Esta es la entrada que se va a agregar al final de la partida
    // Aca se guarda el puntaje y el nombre que va a tomar el template para agregar uno nuevo
    public class PuntajeEntrada {
        // Toma el score del gameManager
        public int score;
        // Toma el nombre del jugador del gameManager
        public string name;
    }
}