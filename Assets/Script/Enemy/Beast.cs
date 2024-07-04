using UnityEngine;

public class Beast : MonoBehaviour, IEnemy
{
    [SerializeField]
    private float moveWhenAttackSpeed = 5.0f; // Enemy movement speed

    private EnemyPathFinding enemyPathFinding;

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        GetComponent<EnemyAI>().stopMovingWhileAttacking = false;
        enemyPathFinding.moveSpeed = moveWhenAttackSpeed;
    }
    public void Attack()
    {

        GetComponent<EnemyAI>().roamPosition = -transform.position + PlayerController.Instance.transform.position;
    }

}


