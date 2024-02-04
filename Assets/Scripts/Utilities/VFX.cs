using UnityEngine;

public class VFX : MonoBehaviour
{

    [SerializeField] private bool m_destroyOnDisable;
    [SerializeField] private float m_vfxLifetime;
    [SerializeField] private bool m_destroyAfterTime;

    private float m_vfxLifetimeTimer;
    private void Update()
    {
        if(m_destroyAfterTime)
        {
            CountVFXLifetimeTimer();
            if(m_vfxLifetimeTimer >= m_vfxLifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CountVFXLifetimeTimer()
    {
        m_vfxLifetimeTimer += Time.deltaTime;
    }

    private void OnDisable()
    {
        if(m_destroyOnDisable) Destroy(gameObject);
    }
}
