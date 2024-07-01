using UnityEngine;

public class Figurehead : MonoBehaviour
{
    private EnemyHealth health;

    [SerializeField]
    private int figureHeadHealth = 100;
    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        health.CurrentHealth = figureHeadHealth;

    }
}
