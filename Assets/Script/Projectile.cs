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

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructible indestructile = other.GetComponent<Indestructible>();

        if (!other.isTrigger && (enemyHealth || indestructile))
        {
            endPosition = transform.position;
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            StartCoroutine(DestroyRoutine());
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
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
