using MoreMountains.Feedbacks;
using UnityEngine;

public class Bullet_Blaster : Bullet
{

    [SerializeField] private MMF_Player m_bulletFeedback;

    [Space(10)]
    [Header("Blaster Bullet one shot settings")]
    
    [SerializeField] private float m_extraDamageShotRangeInSeconds;
    [SerializeField] private int m_extraDamage;

    private void OnEnable()
    {
        PlayBulletFeedback();
    }

    private void PlayBulletFeedback()
    {
        m_bulletFeedback.Initialization();
        m_bulletFeedback.PlayFeedbacks();
    }
    
    protected override void Impact(Collider _otherCollider)
    {
        Health health = _otherCollider.GetComponent<Health>();

        if (health != null)
        {
            if(m_despawnTimer > m_extraDamageShotRangeInSeconds)
            {
                health.TakeDamage(m_extraDamage);
            }
            else
            {
                health.TakeDamage(m_bulletDamage);
                if(_otherCollider.CompareTag("Enemy") && m_doesSlow)
                {
                    health.Slow();
                }
            }
            
        }

        if (m_hasImpactVFX) PlayImpactVFX();
        AudioManager.I.Play(m_impactSFX);

        Destroy(gameObject);
    }
    

}
