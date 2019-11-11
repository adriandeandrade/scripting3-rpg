using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCharacter : MonoBehaviour, IDamageable
    {
        // Inspector Fields
        [Header("Base Character Configuration")]
        [SerializeField] protected float maxHealth;

        // Private Variables
        protected float currentHealth;

        // Components
        private Rigidbody2D rBody;

        // Properties
        public float CurrentHealth => currentHealth;

        private void Awake()
        {
            if(rBody == null)
            {
                rBody = GetComponent<Rigidbody2D>();
            }
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float amount)
        {
            float newHealth = currentHealth - amount;

            if (newHealth > 0)
            {
                currentHealth = newHealth;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void DoKnockback(Vector2 direction, float force)
        {
            rBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}

