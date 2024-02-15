using Killbox.Enums;
using UnityEngine;

public class Enemy_Melee_Wiper_Health : Health_Enemy
{
    [SerializeField] private GameObject m_deathSphere;

    protected override void Die(EDamageTypes _damageType)
    {
        base.Die(_damageType);
        Instantiate(m_deathSphere, transform.position, Quaternion.identity);
    }
}
