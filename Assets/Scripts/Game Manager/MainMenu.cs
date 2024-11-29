using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
