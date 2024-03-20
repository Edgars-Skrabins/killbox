using Killbox.Enums;
using UnityEngine;

public class Bullet_Pippy : Bullet
{
    [SerializeField] private int m_bounces;
    [SerializeField] private float m_sizeIncrease;
    [Range(0, 100)]
    [SerializeField] private float m_chanceOfTurningEnemyIntoFriend;

    protected override void Impact(Collider _otherCollider)
    {
        if (m_hasImpactVFX) PlayImpactVFX();
        if (_otherCollider.TryGetComponent(out Health_Enemy health))
        {
            int randomNum = Random.Range(0, 101);
            if (randomNum <= m_chanceOfTurningEnemyIntoFriend)
            {
                health.TakeDamage(m_bulletDamage, EDamageTypes.BEFRIEND, m_doesCharge);
                AudioManager.I.Play(m_impactSFX);
                Destroy(gameObject);
                return;
            }

            health.TakeDamage(m_bulletDamage, m_damageType, m_doesCharge);
            AudioManager.I.Play(m_impactSFX);
            Destroy(gameObject);
            return;
        }

        if (m_bounces <= 0)
        {
            Destroy(gameObject);
            return;
        }

        IncreaseSize();
        LaunchBullet();
        m_bounces -= 1;
    }

    private void IncreaseSize()
    {
        m_bulletTF.localScale += new Vector3(m_sizeIncrease, m_sizeIncrease, m_sizeIncrease);
    }
}
