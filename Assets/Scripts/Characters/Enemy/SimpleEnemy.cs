using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace enjoii.Characters
{
    public enum EnemyStates
    {
        IDLE, CHARGING, DASHING, STUNNED
    }

    public class SimpleEnemy : BaseEnemy
    {
        // Inspector Fields
        [Header("Simple Enemy Configuration")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashCooldown;
        [SerializeField] private float dashTime = 2f; // How long the dash will last
        [SerializeField] private float maxDashDistance;
        [SerializeField] private float stunDuration;
        [SerializeField] private Color stunnedColor;

        // Private Variables
        private EnemyStates currentState;

        private float currentDashTime;
        private float currentDashCooldownTime;
        private float currentStunTime;

        private Vector2 dashDestination;
        private Vector2 dashDirection;

        private Color originalColor;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            OnTargetLost += OnLostTarget;

            originalColor = spriteRenderer.color;

            target = null;
            currentDashTime = 0;
            SetState(EnemyStates.IDLE);
        }

        protected override void Update()
        {
            base.Update();
            UpdateState();
        }

        private void OnLostTarget()
        {
            SetState(EnemyStates.IDLE);
        }

        private void SetState(EnemyStates newState)
        {
            switch (newState)
            {
                case EnemyStates.IDLE:
                    currentState = EnemyStates.IDLE;
                    currentDashTime = dashTime;
                    spriteRenderer.color = originalColor;
                    break;

                case EnemyStates.CHARGING:
                    currentState = EnemyStates.CHARGING;
                    animator.SetTrigger("Charge");
                    break;

                case EnemyStates.DASHING:
                    currentState = EnemyStates.DASHING;

                    dashDestination = Target.transform.position;
                    dashDirection = (dashDestination - (Vector2)transform.position).normalized;

                    break;

                case EnemyStates.STUNNED:
                    currentState = EnemyStates.STUNNED;
                    currentStunTime = stunDuration;
                    spriteRenderer.color = stunnedColor;
                    break;
            }
        }

        private void UpdateState()
        {
            switch (currentState)
            {
                case EnemyStates.IDLE:
                    if (GetDistanceToTarget() <= maxDashDistance)
                    {
                        SetState(EnemyStates.CHARGING);
                    }

                    break;

                case EnemyStates.CHARGING:
                    break;

                case EnemyStates.DASHING:

                    rBody.velocity = dashDirection * dashSpeed;

                    if (currentDashTime > 0)
                    {
                        currentDashTime -= Time.deltaTime;
                        return;
                    }

                    rBody.velocity = Vector2.zero;
                    currentDashCooldownTime = dashCooldown;
                    SetState(EnemyStates.IDLE);

                    break;

                case EnemyStates.STUNNED:
                    if (currentStunTime > 0)
                    {
                        currentStunTime -= Time.deltaTime;
                        return;
                    }

                    SetState(EnemyStates.IDLE);
                    break;
            }
        }

        // Called by animation event
        public void OnFinishedCharging()
        {
            if (GetDistanceToTarget() <= maxDashDistance)
            {
                SetState(EnemyStates.DASHING);
            }
            else
            {
                SetState(EnemyStates.IDLE);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(5f);

                    Vector2 direction = other.transform.position - transform.position;
                    damageable.DoKnockback(direction, 15f);
                }
            }
        }

        public override void OnHit(GameObject thisGameObject)
        {
            if (currentState == EnemyStates.DASHING)
            {
                SetState(EnemyStates.STUNNED);
                rBody.velocity = Vector2.zero;
                Vector2 direction = thisGameObject.transform.position - transform.position;
                DoKnockback(-direction, 50f);
            }
        }
    }
}