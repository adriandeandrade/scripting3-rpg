using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dashCooldown;
        [SerializeField] private float dashTime = 2f;
        [SerializeField] private float maxDashDistance;
        [SerializeField] private float stunDuration;

        // Private Variables
        private EnemyStates currentState;
        private float currentDashTime;
        private float currentDashCooldownTime;
        private float currentStunTime;
        private GameObject target;
        private Vector2 dashDestination;
        private Vector2 dashDirection;
        private Animator animator;

        // Properties
        private GameObject Target
        {
            get
            {
                if (target == null) target = FindObjectOfType<Player>().gameObject;

                if (target == null)
                {
                    return null;
                }
                else
                {
                    return target;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();

            target = null;
            currentDashTime = 0;
            SetState(EnemyStates.IDLE);
        }

        private void Update()
        {
            UpdateState();
        }

        private void SetState(EnemyStates newState)
        {
            switch (newState)
            {
                case EnemyStates.IDLE:
                    currentState = EnemyStates.IDLE;
                    currentDashTime = dashTime;
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

                    rBody.velocity = dashDirection * moveSpeed;

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

        private float GetDistanceToTarget()
        {
            return Vector2.Distance(Target.transform.position, transform.position);
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


