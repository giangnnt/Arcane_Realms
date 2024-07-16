using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("Character Sample").transform;
        if(PlayerController.Instance.gameObject)
        {
            PlayerController.Instance.gameObject.SetActive(true);
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
