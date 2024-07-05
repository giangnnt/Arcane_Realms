using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        if(GameObject.Find("ActiveInventory") != null)
        {
            GameObject.Find("ActiveInventory").SetActive(true);
        }
        if(GameObject.Find("Gold Coin Container") != null)
        {
            GameObject.Find("Gold Coin Container").SetActive(true);
        }
        if(GameObject.Find("Stamina Container") != null)
        {
            GameObject.Find("Stamina Container").SetActive(true);
        }
        if(GameObject.Find("Heart Container") != null)
        {
            GameObject.Find("Heart Container").SetActive(true);
        }
        if(GameObject.Find("PauseButton") != null)
        {
            GameObject.Find("PauseButton").SetActive(true);
        }
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
