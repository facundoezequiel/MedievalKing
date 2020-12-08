using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaDePuntaje : MonoBehaviour {
    public Transform container;
    public Transform template;
    public List<Transform> puntajeEntradaTransformList;
    public GameManager gameManager;

    // BUSCAR ESTO
    public void Awake () {
        // Busca a estos objectos por su nombre
        container = transform.Find ("PuntajeContainer");
        template = container.Find ("PuntajeTemplate");
        // Apago el template de puntaje
        template.gameObject.SetActive (false);

        // Funcion que agrega una entrada a la lista, le paso el puntaje y el nombre desde el GameManager
        AddPuntajeEntrada (gameManager.puntaje, gameManager.playerName.ToString ());

        string jsonString = PlayerPrefs.GetString ("TablaDePuntaje");
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

        // BUSCAR ESTO
        puntajeEntradaTransformList = new List<Transform> ();
        foreach (PuntajeEntrada puntajeEntrada in puntajes.puntajeEntradaList) {
            CrearEntradaPuntaje (puntajeEntrada, container, puntajeEntradaTransformList);
        }
        // BUSCAR ESTO ESTO CREA UN JSON DE LA LISTA CON TODO LOS PUNTAJES PARA GUARDAR EL JUEGO
        /*
        Puntajes puntajes = new Puntajes { puntajeEntradaList = puntajeEntradaList };
        string json = JsonUtility.ToJson (puntajes);
        PlayerPrefs.SetString ("TablaDePuntaje", json);
        PlayerPrefs.Save ();
        Debug.Log (PlayerPrefs.GetString ("TablaDePuntaje"));
        */
    }

    public void CrearEntradaPuntaje (PuntajeEntrada puntajeEntrada, Transform container, List<Transform> transformList) {
        if ((transformList.Count + 1) <= 10) {
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

            MostrarPuntaje ();

            void MostrarPuntaje () {
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
                transformList.Add (puntajeTransform);
            }
        }
    }

    // Funcion que agrega una entrada de puntaje, es decir, un puntaje nuevo
    public void AddPuntajeEntrada (int score, string name) {
        // Crea un puntaje
        PuntajeEntrada puntajeEntrada = new PuntajeEntrada { score = score, name = name };
        // Carga Guarda Puntajes
        string jsonString = PlayerPrefs.GetString ("TablaDePuntaje");
        Puntajes puntajes = JsonUtility.FromJson<Puntajes> (jsonString);
        // Agrega una nueva entrada
        puntajes.puntajeEntradaList.Add (puntajeEntrada);
        // Guarda y actualiza los puntajes
        string json = JsonUtility.ToJson (puntajes);
        PlayerPrefs.SetString ("TablaDePuntaje", json);
        PlayerPrefs.Save ();
    }

    // Esta es la lista donde se jugardan todos las entradas de puntajes
    public class Puntajes {
        public List<PuntajeEntrada> puntajeEntradaList;
    }

    // Esta es la entrada que se va a agregar al final de la partida
    [System.Serializable]
    public class PuntajeEntrada {
        // Toma el score del gameManager
        public int score;
        // Toma el nombre del jugador del gameManager
        public string name;
    }
}