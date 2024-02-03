using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    
    [Header(" ---- General Health Pickup Settings ----")]
    [Space(5)]

    [SerializeField] private bool m_startActivated;
    [SerializeField] private float m_healthPickupRespawnTime;
    [SerializeField] private GameObject m_healthPickupGFX;
    [SerializeField] private GameObject m_healthPickupTimerGFX;
    [SerializeField] private Image m_healthPickupTimerGFXImage;
    [SerializeField] private GameObject m_pickupVFX;
    
    [SerializeField] private int m_healAmount;

    private bool m_isHealthPickupActive;
    private float m_healthPickupRespawnTimer;

    private void Start()
    {
        if(m_startActivated)
        {
            ActivateHealthPickup();
        }
        else
        {
            DeactivateHealthPickup();
        }
    }

    private void Update()
    {
        if(!m_isHealthPickupActive)
        {
            CountRespawnTimer();
        }
    }

    private void CountRespawnTimer()
    {
        m_healthPickupRespawnTimer += Time.deltaTime;
        if(m_healthPickupRespawnTimer >= m_healthPickupRespawnTime)
        {
            m_healthPickupRespawnTimer = 0;
            ActivateHealthPickup();
        }
        HealthPickupTimerGFX();
    }

    private void OnTriggerStay(Collider other)
    {

        if(!m_isHealthPickupActive) return;

        if(other.CompareTag("Player"))
        {
            if(PlayerStats.I.PlayerHealth < PlayerStats.I.PlayerMaxHealth)
            {
                HealPlayer();
            }
        }
    }

    private void HealPlayer()
    {
        PlayerStats.I.PlayerHealthCS.Heal(m_healAmount);
        
        DeactivateHealthPickup();
        PlayPickupVFX();
    }
    
    private void ActivateHealthPickup()
    {
        m_isHealthPickupActive = true;
        m_healthPickupGFX.SetActive(true);
        m_healthPickupTimerGFX.SetActive(false);
    }

    private void DeactivateHealthPickup()
    {
        m_isHealthPickupActive = false;
        m_healthPickupGFX.SetActive(false);
        m_healthPickupTimerGFX.SetActive(true);
    }

    private void PlayPickupVFX()
    {
        if(m_pickupVFX) Instantiate(m_pickupVFX, transform.position, Quaternion.identity);
    }
    
    
    
    private void HealthPickupTimerGFX()
    {
        m_healthPickupTimerGFXImage.fillAmount = m_healthPickupRespawnTimer / m_healthPickupRespawnTime;
    }
    

}
