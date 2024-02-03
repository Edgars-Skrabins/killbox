using UnityEngine;

public class BarrelLauncher : MonoBehaviour
{

    [SerializeField] private Transform m_barrelSpawnPositionTF;
    [SerializeField] private GameObject m_barrelGO;
    [SerializeField] private float m_barrelSpawnFrequencyInSeconds = 10f;

    private bool m_spawnerActive;
    
    private float m_spawnTimer;
    private void Update()
    {
        if(m_spawnerActive)
        {
            CountTimer();
        }
    }

    private void CountTimer()
    {
        m_spawnTimer += Time.deltaTime;
        if(m_spawnTimer >= m_barrelSpawnFrequencyInSeconds)
        {
            m_spawnTimer = 0;
            DeactivateSpawner();
            SpawnBarrel();
        }
    }

    public void ActivateSpawner()
    {
        m_spawnerActive = true;
    }

    public void DeactivateSpawner()
    {
        m_spawnerActive = false;
    }
    
    public void SpawnBarrel()
    {
        Instantiate(m_barrelGO, m_barrelSpawnPositionTF.position, Quaternion.identity);
    }

}
