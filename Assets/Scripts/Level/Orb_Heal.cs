using UnityEngine;

public class Orb_Heal : Orb
{
    [SerializeField] private int m_healAmount;

    protected override void PlayEffect()
    {
        if (PlayerStats.I.PlayerHealth < PlayerStats.I.PlayerMaxHealth)
        {
            PlayerStats.I.PlayerHealthCS.Heal(m_healAmount);
        }
    }
}