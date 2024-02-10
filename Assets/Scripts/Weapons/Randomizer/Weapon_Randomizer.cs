using UnityEngine;

public class Weapon_Randomizer : ProjectileWeapon
{
    [SerializeField] private GameObject[] m_bulletPrefabs;

    protected override void Shoot()
    {
        m_bulletPrefab = GetRandomBullet();
        base.Shoot();
    }

    private GameObject GetRandomBullet()
    {
        GameObject randomBullet = m_bulletPrefabs[Random.Range(0, m_bulletPrefabs.Length)];
        return randomBullet;
    }
}
