using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSphere : MonoBehaviour
{

    [SerializeField] private int m_sphereDamage;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health health))
        {
            health.TakeDamage(m_sphereDamage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(m_sphereDamage);
        }
    }
}
