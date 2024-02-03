using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : Singleton<ItemsManager>
{
    [SerializeField] private List<HealthPickup> m_healthPickups;
    public List<HealthPickup> Lootboxes {get => m_healthPickups; private set => m_healthPickups = value;}

    [SerializeField] private List<Barrel> m_barrels;
    public List<Barrel> Barrels {get => m_barrels; private set => m_barrels = value;}

}
