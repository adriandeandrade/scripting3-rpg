using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace enjoii.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCharacter : MonoBehaviour, IDamageable
    {
        // Inspector Fields
        [Header("Base Character Configuration")]
        [SerializeField] protected float maxHealth = 100;
        [SerializeField] private Image healthBar;

        // Private Variables
        protected float currentHealth;

        // Components
        protected Rigidbody2D rBody;

        // Properties
        public float CurrentHealth => currentHealth;

        protected virtual void Awake()
        {
            if(rBody == null)
            {
                rBody = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void Start()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float amount)
        {
            float newHealth = currentHealth - amount;

            if (newHealth > 0)
            {
                RecalculateHealth(newHealth);
            }
            else
            {
                Kill();
            }
        }

        public virtual void IncreaseHealth(float amount)
        {
            float newHealth = currentHealth + amount;

            if(newHealth > maxHealth)
            {
                newHealth = Mathf.Clamp(newHealth, 0, maxHealth);
            }

            RecalculateHealth(newHealth);
        }

        public virtual void Kill()
        {
            Destroy(gameObject);
        }

        private void RecalculateHealth(float newAmount)
        {
            currentHealth = newAmount;
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        public void DoKnockback(Vector2 direction, float force)
        {
            rBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }

        public virtual void OnHit(GameObject thisGameObject)
        {

        }
    }
}

