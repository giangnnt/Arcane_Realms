using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("City Scene");
    }

    public void Option()
    {

    }

    public void Load()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
