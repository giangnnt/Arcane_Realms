using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class SceneMangagement : Singleton<SceneMangagement>
{
    public static SceneMangagement instance;
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string transitionName)
    {
        this.SceneTransitionName = transitionName;
    }
    public void Nextlevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
