using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enjoii.Characters
{
    public class PlayerController : MonoBehaviour
    {
        // Inspector Fields
        [SerializeField] private float runSpeed = 2f;
        [SerializeField] private float groundDamping = 15f;

        // Private Variables
        private float currentSpeed;
        private float facingDirection;

        // Facing Direction Context
        private readonly Vector2 up = new Vector2(0, 1);
        private readonly Vector2 down = new Vector2(0, -1);
        private readonly Vector2 left = new Vector2(1, 0);
        private readonly Vector2 right = new Vector2(-1, 0);

        // Properties
        public float FacingDirection => facingDirection;
        public Vector2 MovementInput => input;
        public Vector2 Velocity => velocity;

        // Components
        private Rigidbody2D rBody2D;
        private SpriteRenderer spriteRenderer;
        private Camera cam;
        private Vector2 velocity;
        private Vector2 input;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rBody2D = GetComponent<Rigidbody2D>();
            cam = Camera.main;
        }

        private void Start()
        {
            currentSpeed = runSpeed;
        }

        private void FixedUpdate()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            FlipTowardsMouse();
            SetFacingDirection(input);

            velocity = Vector2.Lerp(velocity, input.normalized * runSpeed, Time.deltaTime * groundDamping);
            rBody2D.velocity = velocity;
        }

        private void SetFacingDirection(Vector2 _velocity)
        {
            if (_velocity == Vector2.zero) return;

            if (_velocity == down) facingDirection = 0f;
            if (_velocity == right) facingDirection = 1f;
            if (_velocity == left) facingDirection = 2f;
            if (_velocity == up) facingDirection = 3f;
        }

        private void FlipTowardsMouse()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = cam.WorldToScreenPoint(transform.position);

            if (mousePos.x < objectPos.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (mousePos.x > objectPos.x)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}

