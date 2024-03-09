using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField] private Transform m_orbSpawnLocation;
    [SerializeField] private float m_orbSpawnRate;
    [SerializeField] private GameObject[] m_orbs;

    private GameObject m_currentOrb;
    private bool m_isOrbAlive;
    private float m_spawnTimer;

    private void Update()
    {
        CountSpawnTimer();
        CheckOrbStatus();
    }

    private void CheckOrbStatus()
    {
        if (m_currentOrb == null)
        {
            SetIsOrbAlive(false);
        }
    }

    private void CountSpawnTimer()
    {
        if (m_isOrbAlive)
        {
            return;
        }

        if (m_spawnTimer >= m_orbSpawnRate)
        {
            SpawnOrb();
            m_spawnTimer = 0;
        }
        else
        {
            m_spawnTimer += Time.deltaTime;
        }
    }

    private void SpawnOrb()
    {
        SetIsOrbAlive(true);
        int randomOrbIndex = Random.Range(0, m_orbs.Length);
        m_currentOrb = Instantiate(m_orbs[randomOrbIndex], m_orbSpawnLocation.position, Quaternion.identity);
    }

    public void SetIsOrbAlive(bool _isOrbAlive)
    {
        m_isOrbAlive = _isOrbAlive;
    }
}