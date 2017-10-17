using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LoadScene(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void LoadRandomScene() {
        int index = Random.Range(1, SceneManager.sceneCount);
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    
    public void Quit() {
        Application.Quit();
    }
}
