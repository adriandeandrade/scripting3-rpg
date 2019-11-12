using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void DoKnockback(Vector2 direction, float force);
    void OnHit(GameObject thisGameObject);
}
