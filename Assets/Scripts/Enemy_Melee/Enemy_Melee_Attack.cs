using UnityEngine;

public class Enemy_Melee_Attack : MonoBehaviour
{

    [SerializeField] private Transform m_attackOriginTF;

    [SerializeField] private int m_damage;
    [SerializeField] private float m_attackDistance;
    [SerializeField] private GameObject m_deathVFX;
    [SerializeField] private LayerMask m_damageableLayers;


    private void Update()
    {
        
        Attack();
        
#if UNITY_EDITOR
        
        DebugGizmos();

#endif

    }

    [SerializeField] private GameObject m_enemyDebri;
    
    private void Attack()
    {
        if (Physics.Raycast(m_attackOriginTF.position, m_attackOriginTF.forward, out var hit, m_attackDistance, m_damageableLayers))
        {
            Health health = hit.transform.GetComponent<Health>();
            health.TakeDamage(m_damage);
            Instantiate(m_deathVFX, transform.position, Quaternion.identity);
            AudioManager.I.Play("Explode_Enemy");
            Instantiate(m_enemyDebri, transform.position, Random.rotation);
            gameObject.SetActive(false);
        }
    }

    private void DebugGizmos()
    {
        Debug.DrawRay(m_attackOriginTF.position, m_attackOriginTF.forward * m_attackDistance, Color.red);
    }

}
