using UnityEngine;

public class Weapon_ElectroGun : HitscanWeapon
{

    [Space(10)]
    [Header("ElectroGun Settings")]
    [Space(5)]
    [SerializeField] private AnimationCurve m_damageDropOff;
    
    
    protected override void Shoot()
    {

        if(!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        m_fireRateTimer = 0;
        
        if(Physics.Raycast(m_firePointTF.position, m_firePointTF.forward,out var hit,Mathf.Infinity, m_damageLayer))
        {
            bool inWeaponRange = (hit.point - m_firePointTF.position).magnitude <= m_weaponRange;

            float distanceToEnemy = (hit.point - m_firePointTF.position).magnitude;

            if (inWeaponRange)
            {
                Health health = hit.collider.GetComponent<Health>();
            
                if(health)
                {
                    float damage = m_damageDropOff.Evaluate(distanceToEnemy);
                    health.TakeDamage(Mathf.CeilToInt(damage));
                    if(hit.collider.CompareTag("Enemy"))
                    {
                        health.Slow();
                    }
                }
            }

        }
        
        if(m_hasProjectileVFX) PlayProjectileVFX();
        if(m_hasHitImpactVFX) PlayImpactVFX(hit);
        if(m_hasShotAnimation) PlayShotAnimation();
        if(m_hasShotFeedback) PlayShotFeedback();
        if(m_hasMuzzleVFX) PlayMuzzleVFX();

        AudioManager.I.Play(m_shootSFX);
    }


    protected override void PlayMuzzleVFX()
    {
        if (m_muzzleVFXArray.Length <= 0) return;

        foreach (var vfx in m_muzzleVFXArray)
        {
            var obj = Instantiate(vfx, m_muzzleTF.position, m_muzzleTF.rotation);
            obj.transform.SetParent(m_muzzleTF);
        }

    }

}
