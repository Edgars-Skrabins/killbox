using UnityEngine;

public class Weapon_RocketLauncher : ProjectileWeapon
{

    private int m_activeFirePointIndex;
    private Transform m_activeFirePointTF;

    private void Start()
    {
        m_activeFirePointTF = m_firePoints[m_activeFirePointIndex];
    }

    protected override void Shoot()
    {
        
        if(!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;
        
        m_fireRateTimer = 0f;
        
        Instantiate(m_bulletPrefab, m_activeFirePointTF.position, m_activeFirePointTF.rotation);

        if(m_activeFirePointIndex + 1 >= m_firePoints.Length)
        {
            m_activeFirePointIndex = 0;
        }
        else
        {
            m_activeFirePointIndex += 1;
        }

        m_activeFirePointTF = m_firePoints[m_activeFirePointIndex];

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
            var obj = Instantiate(vfx, m_activeFirePointTF.position, m_activeFirePointTF.rotation);
            obj.transform.SetParent(m_activeFirePointTF);
        }

    }
}
