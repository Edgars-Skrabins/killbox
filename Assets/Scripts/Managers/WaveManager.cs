using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private Transform[] m_spawnPoints;

    private float m_spawnTimer;

    [Serializable]
    public class EnemiesToSpawn
    {
        public string m_EnemyName;

        public AnimationCurve m_SpawnFrequency;
        public AnimationCurve m_maxAmountOfEnemies;

        [HideInInspector] public float m_SpawnTimer;
    }

    public EnemiesToSpawn[] m_enemiesToSpawn;

    private void Update()
    {
        CountSpawnTimer();
    }

    private void CountSpawnTimer()
    {
        for (int i = 0; i < m_enemiesToSpawn.Length; i++)
        {
            EnemiesToSpawn enemy = m_enemiesToSpawn[i];

            if (enemy.m_SpawnTimer >= enemy.m_SpawnFrequency.Evaluate(GameManager.I.TimeSinceGameStart)
                && EnemyManager.I.GetEnemyList(enemy.m_EnemyName).Count <
                enemy.m_maxAmountOfEnemies.Evaluate(GameManager.I.TimeSinceGameStart))
            {
                enemy.m_SpawnTimer = 0f;
                SpawnEnemy(i);
            }
            else
            {
                enemy.m_SpawnTimer += Time.deltaTime;
            }
        }
    }

    public void SpawnEnemy(int _enemyArrayIndex)
    {
        GameObject obj = ObjectPoolManager.I.GetPooledObject(m_enemiesToSpawn[_enemyArrayIndex].m_EnemyName);
        obj.transform.position = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].position;
        obj.SetActive(true);
    }
}