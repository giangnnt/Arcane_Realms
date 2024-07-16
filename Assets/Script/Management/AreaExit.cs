using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private string sceneTransitionName;
    [SerializeField]
    private bool checkRemainingEnemy = false;
    private float waitToLoadTime = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        checkRemainingEnemy = true;
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (checkRemainingEnemy)
            {
                if (!FindAnyObjectByType<EnemyAI>())
                {
                    SceneMangagement.Instance.SetTransitionName(sceneTransitionName);
                    UIFade.Instance.FadeToBlack();
                    StartCoroutine(LoadSceneRoutine());
                };
            }
            else
            {
                SceneMangagement.Instance.SetTransitionName(sceneTransitionName);
                UIFade.Instance.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());
            }
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
