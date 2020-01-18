using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene(1);
        Debug.Log("THIS IS WROKING");
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void MenuScreen() {
        SceneManager.LoadScene(0);
    }
}
