using Killbox.Enums;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public abstract void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget);
    protected abstract void Die(EDamageTypes _damageType);
}
