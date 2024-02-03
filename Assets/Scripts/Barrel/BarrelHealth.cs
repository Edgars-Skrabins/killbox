using System;
using UnityEngine;

public class BarrelHealth : Health
{

    [SerializeField] private BarrelStats m_barrelStatsCS;

    public override void TakeDamage(int _damage)
    {
        m_barrelStatsCS.BarrelHealth -= _damage;
        if(m_barrelStatsCS.BarrelHealth <= 0)
        {
            Die();
        }
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        throw new NotImplementedException();
    }

    public override void Slow()
    {
        
    }

    public override void Stun()
    {
        throw new NotImplementedException();
    }

    private bool m_hasExploded;
    
    protected override void Die()
    {
        if(m_hasExploded) return;
        m_hasExploded = true;
        m_barrelStatsCS.m_barrelCS.Explode();
    }

}
