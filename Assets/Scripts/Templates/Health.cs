using UnityEngine;

public abstract class Health : MonoBehaviour
{
    
    public abstract void TakeDamage(int _damage);
    
    public abstract void TakeDamage(int _damage, int _explosiveDamage);
    
    protected abstract void Die();

    public abstract void Slow();

    public abstract void Stun();

}
