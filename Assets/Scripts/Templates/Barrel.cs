using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [Header(" Barrel Explosion Settings")]
    [Space(5)]
    [SerializeField] private int m_explosionDamage;
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private LayerMask m_explosionLayers;

    [Space(20)]

    [Header(" Explosion VFX Settings ")]
    [Space(5)]
    [SerializeField] private GameObject[] m_explosionVFXArray;
    [SerializeField] private string m_explosionSFX;

    public virtual void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius, m_explosionLayers);

        foreach(var hit in colliders)
        {
            Health health = hit.GetComponent<Health>();
            
            if(health)
            {
                health.TakeDamage(m_explosionDamage);
            }
        }

        AudioManager.I.Play(m_explosionSFX);
        PlayExplosionVFX();
        Destroy(gameObject);
        
    }

    protected virtual void PlayExplosionVFX()
    {
        if(m_explosionVFXArray.Length <= 0) return; 
        
        foreach(var vfx in m_explosionVFXArray)
        {
            Instantiate(vfx, transform.position, Quaternion.identity);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}
