using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    
    [Serializable]
    public class EnemyLists
    {
        public string m_EnemyName;
        [HideInInspector] public List<GameObject> m_EnemyList;
    }

    public EnemyLists[] m_EnemyLists;

    public List<GameObject> GetEnemyList(string _enemyListName)
    {
        var list = Array.Find(m_EnemyLists, EnemyList => EnemyList.m_EnemyName == _enemyListName);
        return list.m_EnemyList;
    }
    
    
}
