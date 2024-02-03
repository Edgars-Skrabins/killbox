using UnityEngine;

public class EnemyShatter : MonoBehaviour
{

    [SerializeField] private Transform m_explosionCenter;
    [SerializeField] private float m_explosionForce;
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private float m_upwardsModifier;

    private void OnEnable()
    {
        ExplodeDebri();
    }

    private void ExplodeDebri()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius,LayerMask.GetMask("Debri"));
        
        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(m_explosionForce, m_explosionCenter.position,m_explosionRadius,m_upwardsModifier);
            }
        }
    }

    [SerializeField] private float m_selfDestructInSeconds;
    private float m_selfDestructTimer;
    
    private void Update()
    {
        CountSelfDestructTimer();
    }

    private void CountSelfDestructTimer()
    {
        m_selfDestructTimer += Time.deltaTime;
        if(m_selfDestructTimer >= m_selfDestructInSeconds) Destroy(gameObject);
    }
}
