using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class HitscanWeapon : MonoBehaviour
{

    [Header(" ----- Firing Settings -----")]
    [Space(5)]
    [SerializeField] protected Transform m_firePointTF;
    [SerializeField] protected int m_weaponDamage;
    [SerializeField] protected float m_fireRateInSeconds;
    [SerializeField] protected float m_weaponRange;
    [SerializeField] protected LayerMask m_damageLayer;
    [SerializeField] protected LayerMask m_firePointVisibleLayer;

    [Space(20)]

    [Header(" ----- Shot Animation Settings -----")]
    [Space(5)]

    [SerializeField] protected bool m_hasShotAnimation;
    [SerializeField] protected WeaponMovement m_weaponMovementCS;

    [Space(20)]
    [Header(" ----- Shot Feedback Settings -----")]
    [Space(5)]

    [SerializeField] protected bool m_hasShotFeedback;
    [SerializeField] protected MMF_Player m_shotFeedback;

    [Space(20)]
    [Header(" ----- Shot ImpactVFX Settings -----")]
    [Space(5)]
    
    [SerializeField] protected bool m_hasHitImpactVFX;
    [SerializeField] protected GameObject[] m_hitImpactVFXArray;
    [SerializeField] protected string m_impactSFX;

    [Space(20)]
    [Header(" ----- Muzzle VFX Settings -----")]
    [Space(5)]

    [SerializeField] protected bool m_hasMuzzleVFX;
    [SerializeField] protected GameObject[] m_muzzleVFXArray;
    [SerializeField] protected Transform m_muzzleTF;

    [Space(20)]
    [Header(" ----- Shoot ProjectileVFX Settings -----")]
    [Space(5)]
    [SerializeField] protected bool m_hasProjectileVFX;
    [SerializeField] protected GameObject[] m_ProjectileVFXArray;
    [SerializeField] protected string m_shootSFX;

    [Space(20)]
    [Header(" ----- FirePointVisibleSettings ----- ")]
    [Space(5)]

    [SerializeField] private GameObject m_crossHairImage;
    [SerializeField] private GameObject m_cantShootImage;

    protected float m_fireRateTimer;

    protected void Awake()
    {
        m_fireRateTimer = m_fireRateInSeconds;
    }

    protected virtual void Update()
    {

        if(GameManager.I.IsGamePaused) return;
        
        FirePointVisibleGFX();
        CountFireTimer();
        Inputs();
    }

    protected virtual void FirePointVisibleGFX()
    {
        if(CheckIfFirePointVisible())
        {
            if(!m_crossHairImage.activeInHierarchy)
            {
                m_crossHairImage.SetActive(true);
            }

            if(m_cantShootImage.activeInHierarchy)
            {
                m_cantShootImage.SetActive(false);
            }

        }
        else
        {
            if(!m_cantShootImage.activeInHierarchy)
            {
                m_cantShootImage.SetActive(true);
            }

            if(m_crossHairImage.activeInHierarchy)
            {
                m_crossHairImage.SetActive(false);
            }
        }
    }
    
    protected virtual bool CheckIfFirePointVisible()
    {

        Vector3 cameraToMuzzle = m_firePointTF.position - PlayerStats.I.PlayerCamera.transform.position;
        
        if(Physics.Raycast(PlayerStats.I.PlayerCamera.transform.position, cameraToMuzzle,out RaycastHit hit, 100f, m_firePointVisibleLayer))
        {
            if(hit.collider.CompareTag("Muzzle"))
            {
                return true;
            }
        }
        
        return false;
    }

    protected virtual void CountFireTimer()
    {
        m_fireRateTimer += Time.deltaTime;
    }

    protected virtual void Inputs()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot())
        {
            Shoot();
        }
    }

    protected virtual bool CanShoot()
    {
        if (m_fireRateTimer >= m_fireRateInSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Shoot()
    {

        if(!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;
        
        m_fireRateTimer = 0f;

        if (Physics.Raycast(m_firePointTF.position, m_firePointTF.forward, out var hit,Mathf.Infinity, m_damageLayer))
        {
            bool inWeaponRange = (hit.point - m_firePointTF.position).magnitude <= m_weaponRange;
            
            if (inWeaponRange)
            {
                Health health = hit.collider.GetComponent<Health>();

                if (health)
                {
                    health.TakeDamage(m_weaponDamage);
                }
            }

        }
        
        if (m_hasHitImpactVFX) PlayImpactVFX(hit);
        if (m_hasProjectileVFX) PlayProjectileVFX();
        if (m_hasShotAnimation) PlayShotAnimation();
        if (m_hasShotFeedback) PlayShotFeedback();
        if (m_hasMuzzleVFX) PlayMuzzleVFX();

        AudioManager.I.Play(m_shootSFX);
    }

    protected virtual void PlayShotAnimation()
    {
        if (m_weaponMovementCS) m_weaponMovementCS.PlayShotAnimation();
    }

    protected virtual void PlayShotFeedback()
    {
        if (m_shotFeedback) m_shotFeedback.PlayFeedbacks();
    }
    protected virtual void PlayImpactVFX(RaycastHit _hit)
    {
        if (m_hitImpactVFXArray.Length <= 0) return;

        float vfxOffsetFromWall = 0.1f;
        
        foreach (var vfx in m_hitImpactVFXArray)
        {
            var obj = Instantiate(vfx, _hit.point, Quaternion.identity);
            Transform objTF = obj.transform;
            Vector3 playerDir = PlayerStats.I.PlayerTF.position - objTF.position;
            objTF.position += playerDir * vfxOffsetFromWall;
        }
        
        AudioManager.I.Play(m_impactSFX);
    }

    protected virtual void PlayMuzzleVFX()
    {
        if (m_muzzleVFXArray.Length <= 0) return;

        foreach (var vfx in m_muzzleVFXArray)
        {
            var obj = Instantiate(vfx, m_muzzleTF.position, m_muzzleTF.rotation);
            obj.transform.SetParent(m_muzzleTF);
        }

    }

    protected virtual void PlayProjectileVFX()
    {
        if (m_ProjectileVFXArray.Length <= 0) return;

        foreach (var vfx in m_ProjectileVFXArray)
        {
            Instantiate(vfx, m_muzzleTF.position, m_muzzleTF.rotation);
        }

    }

    private void OnEnable()
    {
        m_fireRateTimer = m_fireRateInSeconds;
    }
}
