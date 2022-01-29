using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButton()
    {
        // Start the game by loading Main scene
        SceneManager.LoadScene((int) Levels.Main);
    }

    public void SettingsButton()
    {
        // Open the settings menu
    }

    public void CreditsButton()
    {
        // Open the credits menu
        
    }

    public void ExitButton()
    {
        // Exit the game
        Application.Quit();
    }
}