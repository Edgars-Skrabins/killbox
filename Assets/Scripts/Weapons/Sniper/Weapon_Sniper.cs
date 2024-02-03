using UnityEngine;

public class Weapon_Sniper : HitscanWeapon
{
    [Space(10)]
    [Header("Sniper settings")]
    [Space(5)]

    [SerializeField] private GameObject m_sniperShotLine;
    [SerializeField] private LayerMask m_sniperShotLineLayerMask;
    
    protected override void Shoot()
    {
        
        if(!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;
        
        m_fireRateTimer = 0f;

        // Object raycast
        if(Physics.Raycast(m_firePointTF.position, m_firePointTF.forward, out RaycastHit hit, Mathf.Infinity, m_sniperShotLineLayerMask))
        {
            var vfx = Instantiate(m_sniperShotLine,m_firePointTF.position,Quaternion.identity);

            if(hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(m_weaponDamage);
            }
        
            if(vfx.TryGetComponent(out LineRenderer line))
            {
                line.SetPosition(0, m_firePointTF.position);
                line.SetPosition(1, hit.point);
            }
            
            if (m_hasHitImpactVFX) PlayImpactVFX(hit);
        }
        else
        {
            var vfx = Instantiate(m_sniperShotLine,m_firePointTF.position,Quaternion.identity);
        
            if(vfx.TryGetComponent(out LineRenderer line))
            {
                line.SetPosition(0, m_firePointTF.position);
                line.SetPosition(1, m_firePointTF.position + m_firePointTF.forward  * 100);
            }
        }
        
        Vector3 raycastDirection = hit.point - m_firePointTF.position;

        RaycastHit[] hits;
        // Enemy raycast
        hits = Physics.RaycastAll(m_firePointTF.position, m_firePointTF.forward, raycastDirection.magnitude, m_damageLayer);
        
        foreach(var raycastAllHit in hits)
        {
            if(raycastAllHit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(m_weaponDamage);
                if (m_hasHitImpactVFX) PlayImpactVFX(raycastAllHit);
            }
        }
        
        //if (m_hasProjectileVFX) PlayProjectileVFX(hit);
        if (m_hasShotAnimation) PlayShotAnimation();
        if (m_hasShotFeedback) PlayShotFeedback();
        if (m_hasMuzzleVFX) PlayMuzzleVFX();

        AudioManager.I.Play(m_shootSFX);
    }

    protected void PlayProjectileVFX(RaycastHit _hit)
    {

        var vfx = Instantiate(m_sniperShotLine,m_firePointTF.position,Quaternion.identity);
        
        if(vfx.TryGetComponent(out LineRenderer line))
        {
            line.SetPosition(0, m_firePointTF.position);
            line.SetPosition(1, _hit.point);
        }
    }
}
