using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuButtonActions : MonoBehaviour {

    // setting volume
    public AudioMixer audioMixer;

    public void setVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
    }

    // fullscreen toggle
    public void setFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

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
