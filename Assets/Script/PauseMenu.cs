using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        pauseMenu.SetActive(false);
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
        SceneManager.LoadScene("MenuScene");
    }
}
