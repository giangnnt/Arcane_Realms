using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class FinishPonit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goNextLevel)
        {
            SceneMangagement.Instance.Nextlevel();
        }
        else
        {
            SceneMangagement.Instance.LoadScene(levelName);
        }
    }
}
