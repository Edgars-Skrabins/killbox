using MoreMountains.Feedbacks;
using UnityEngine;

public class WeaponRandomizer : MonoBehaviour
{
    [SerializeField] public float m_randomizeFrequencyInSeconds;
    [HideInInspector] public float m_randomizeTimer;
    [SerializeField] private MMF_Player m_weaponClockFeedback;

    [SerializeField] private string m_nextWeapon;
    [SerializeField] private GameObject m_weaponTextUI;

    private bool m_weaponIndicatorEnabled;

    private void Start()
    {
        m_randomizeTimer = m_randomizeFrequencyInSeconds;
        m_nextWeapon = WeaponManager.I.GetRandomWeaponName();
        WeaponManager.I.ShowNextWeaponIcon(m_nextWeapon);

        m_weaponIndicatorEnabled = PlayerPrefs.GetInt(WeaponIndicatorUI.M_WINDICATOR_PREF) != 0;
        m_weaponTextUI.SetActive(m_weaponIndicatorEnabled);
    }

    private void Update()
    {
        if (GameManager.I.IsGamePaused) return;

        CountRandomizerTimer();
    }

    private void CountRandomizerTimer()
    {
        m_randomizeTimer -= Time.deltaTime;

        if (m_randomizeTimer <= 3.2f)
        {
            if (!AudioManager.I.Playing("WeaponChange"))
            {
                AudioManager.I.Play("WeaponChange");
                m_weaponClockFeedback.Initialization();
                m_weaponClockFeedback.PlayFeedbacks();
            }
        }
        if (m_randomizeTimer <= 0)
        {
            WeaponManager.I.EquipWeapon(m_nextWeapon);
            m_nextWeapon = WeaponManager.I.GetRandomWeaponName();
            WeaponManager.I.ShowNextWeaponIcon(m_nextWeapon);
            m_randomizeTimer = m_randomizeFrequencyInSeconds;

            m_weaponIndicatorEnabled = PlayerPrefs.GetInt(WeaponIndicatorUI.M_WINDICATOR_PREF) != 0;
            m_weaponTextUI.SetActive(m_weaponIndicatorEnabled);
        }

    }
}
