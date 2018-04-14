using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour {
    public void ActionStart()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void ActionQuit()
    {
        Application.Quit();
    }
}
