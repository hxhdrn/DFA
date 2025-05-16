using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;

    public void Play()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void ReturnToMain()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
