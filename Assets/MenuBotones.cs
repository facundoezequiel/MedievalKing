using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBotones : MonoBehaviour {
    public void NewGame () {
        SceneManager.LoadScene ("Level 1");
    }
    public void Replay () {
        SceneManager.LoadScene ("Menu");
    }
}