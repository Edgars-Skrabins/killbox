using UnityEngine;

public class Weapon_Shotgun : ProjectileWeapon
{
    [Space(20)]
    [Header("Shot spread settings")]
    [Space(5)]
    [SerializeField] private float m_randomBulleSpreadX;
    [SerializeField] private float m_randomBulleSpreadY;

    private Transform m_weaponTF;

    private void Start()
    {
        m_weaponTF = transform;
    }

    protected override void Shoot()
    {
        
        if(!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;
        
        m_fireRateTimer = 0f;
        
        foreach (var firePoint in m_firePoints)
        {

            //Rotate the firePoint transform before instantiating the shotgun bullets
            var bulletRotation = firePoint.localEulerAngles;
            bulletRotation.x = m_weaponTF.localEulerAngles.x + Random.Range(-m_randomBulleSpreadX, m_randomBulleSpreadX);
            bulletRotation.y = m_weaponTF.localEulerAngles.y + Random.Range(-m_randomBulleSpreadY, m_randomBulleSpreadY);
            firePoint.localEulerAngles = bulletRotation;

            Instantiate(m_bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (m_hasShotAnimation) PlayShotAnimation();
        if (m_hasShotFeedback) PlayShotFeedback();
        if (m_hasMuzzleVFX) PlayMuzzleVFX();

        AudioManager.I.Play(m_shootSFX);
    }
}
