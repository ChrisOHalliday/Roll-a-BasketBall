using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void PlayGame() 
    {
        if (sceneIndex >= 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
       Application.Quit();
    }

}
