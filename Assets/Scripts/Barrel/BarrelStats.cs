using UnityEngine;

public class BarrelStats : MonoBehaviour
{

    public Barrel m_barrelCS;

    [SerializeField] private int m_barrelHealth;
    public int BarrelHealth {get => m_barrelHealth; set => m_barrelHealth = value;}
    
    [SerializeField] private int m_barrelStartingHealth;

    private void Awake()
    {
        m_barrelHealth = m_barrelStartingHealth;
    }
}
