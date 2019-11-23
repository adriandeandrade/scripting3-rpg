using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace enjoii.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCharacter : MonoBehaviour, IDamageable
    {
        // Inspector Fields
        [Header("Base Character Configuration")]
        [SerializeField] private string characterName;
        [SerializeField] protected float maxHealth = 100;

        [Header("Character UI Configuration")]
        [SerializeField] protected Image healthBar;
        [SerializeField] protected GameObject healthBarContainer;
        [SerializeField] private TextMeshProUGUI characterNameText;

        // Private Variables
        protected float currentHealth;

        // Components
        protected Rigidbody2D rBody;

        // Properties
        public float CurrentHealth => currentHealth;
        public string CharacterName { get => characterName; set => characterName = value; }

        protected virtual void Awake()
        {
            if (rBody == null)
            {
                rBody = GetComponent<Rigidbody2D>();
            }

            if (characterNameText != null)
            {
                characterNameText.SetText(characterName);
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

            if (newHealth > maxHealth)
            {
                newHealth = Mathf.Clamp(newHealth, 0, maxHealth);
            }

            RecalculateHealth(newHealth);
        }

        public virtual void IncreaseMaxHealth(float newAmount)
        {
            maxHealth = newAmount;
            UpdateHealthbarFillAmount();
        }

        public virtual void Kill()
        {
            Destroy(gameObject);
        }

        private void RecalculateHealth(float newAmount)
        {
            currentHealth = newAmount;
            UpdateHealthbarFillAmount();
        }

        private void UpdateHealthbarFillAmount()
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        public void DoKnockback(Vector2 direction, float force)
        {
            rBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }

        public void EnableHealthBar()
        {
            if (!healthBarContainer.activeSelf)
            {
                healthBarContainer.SetActive(true);
            }
        }

        public void DisableHealthbar()
        {
            if (healthBarContainer.activeSelf)
            {
                healthBarContainer.SetActive(false);
            }
        }

        public virtual void OnHit(GameObject thisGameObject)
        {

        }
    }
}

