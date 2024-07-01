public class SceneMangagement : Singleton<SceneMangagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string transitionName)
    {
        this.SceneTransitionName = transitionName;
    }
}
