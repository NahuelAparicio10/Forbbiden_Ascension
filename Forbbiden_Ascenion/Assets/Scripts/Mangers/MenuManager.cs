using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ExitGame() => Application.Quit();

    public void StartGame()
    {
        if(GameManager.Instance.hasStartedGame)
        {
            SceneManager.LoadScene(2);
        }

        GameManager.Instance.hasStartedGame = true;
        SceneManager.LoadScene(1);
    }

}
