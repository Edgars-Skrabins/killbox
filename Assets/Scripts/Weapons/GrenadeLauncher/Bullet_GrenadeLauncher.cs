using MoreMountains.Feedbacks;
using UnityEngine;

public class Bullet_GrenadeLauncher : Bullet
{
    [SerializeField] private int m_explosionDamage;
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private LayerMask m_explosionLayers;
    [SerializeField] private MMF_Player m_bulletAnimation;
    [SerializeField] private int m_bounces;
    [SerializeField] private GameObject m_explosionVFX;
    [SerializeField] private GameObject m_explosionSFX;
    [SerializeField] private AudioSource m_audioSource;
    private bool m_isExploding;

    protected override void Start()
    {
        base.Start();
        m_bulletAnimation.Initialization();
        m_bulletAnimation.PlayFeedbacks();
    }

    protected override void Update()
    {
        if (m_lifeTimeDestroy)
        {
            m_despawnTimer += Time.deltaTime;
            if (m_despawnTimer >= m_bulletLifeTime)
            {
                m_despawnTimer = 0;
                Explode();
            }
        }
    }

    protected override void Impact(Collider _otherCollider)
    {
        if (m_hasImpactVFX) PlayImpactVFX();

        Health health = _otherCollider.GetComponent<Health>();
        if (health != null && !_otherCollider.CompareTag("Player"))
        {
            health.TakeDamage(m_bulletDamage, m_damageType, m_doesCharge);
        }

        m_bounces--;
        m_audioSource.Play();

        if (m_bounces <= 0)
        {
            Explode();
        }

        if (_otherCollider.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    [Space(10)]
    [Header("Debris explosion settings")]
    [Space(5)]
    [SerializeField] private float m_explosionForce;
    [SerializeField] private float m_upwardsModifier;

    private void Explode()
    {
        if (!m_isExploding)
        {
            m_isExploding = true;

            Transform bulletTransform = transform;

            Collider[] colliders = Physics.OverlapSphere(bulletTransform.position, m_explosionRadius);
            Collider[] debrisColliders = Physics.OverlapSphere(bulletTransform.position, m_explosionRadius, LayerMask.GetMask("Debri"));

            foreach (Collider debriCollider in debrisColliders)
            {
                if (debriCollider.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(m_explosionForce, bulletTransform.position, m_explosionRadius, m_upwardsModifier);
                }
            }

            foreach (Collider hit in colliders)
            {
                Health health = hit.GetComponent<Health>();

                if (health && !hit.CompareTag("Player"))
                {
                    health.TakeDamage(m_explosionDamage, m_damageType, m_doesCharge);
                }
            }

            if (m_explosionVFX)
            {
                Instantiate(m_explosionVFX, gameObject.transform.position, m_explosionVFX.transform.rotation);
            }

            Instantiate(m_explosionSFX, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}
