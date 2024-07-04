using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 22f;
    [SerializeField]
    private GameObject particleOnHitPrefabVFX;
    [SerializeField]
    private float destroyDelayTime = 0f;
    [SerializeField]
    private bool isEnemyProjectile = false;
    [SerializeField]
    private float projectileRange = 10f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructible indestructile = other.GetComponent<Indestructible>();
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (!other.isTrigger && enemyHealth || indestructile || player)
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                StartCoroutine(DestroyRoutine());

            }
            else if (!other.isTrigger && indestructile)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                StartCoroutine(DestroyRoutine());
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            StartCoroutine(DestroyRoutine());
        }
    }
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private IEnumerator DestroyRoutine()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        moveSpeed = 0;
        yield return new WaitForSeconds(destroyDelayTime);
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<KeepParticleAlive>().DeactiveParticleSystem();
        }
        Destroy(gameObject);
    }
}
