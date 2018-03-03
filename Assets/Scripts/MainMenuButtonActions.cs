using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuButtonActions : MonoBehaviour {

    // play button, back button, and settings button all load other scenes
    public void LoadGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Quit button
    public void CloseGame()
    {
        Application.Quit();
    }
}
