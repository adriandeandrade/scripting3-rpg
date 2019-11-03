using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31; // CharacterController2D Creator

public class PlayerController : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float groundDamping = 15f;

    // Private Variables
    private float currentSpeed;

    // Components
    private CharacterController2D controller2D;
    private RaycastHit2D lastControllerColliderHit;
    private Vector2 velocity;
    private Vector2 input;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
        controller2D.onControllerCollidedEvent += OnControllerCollider;
        controller2D.onTriggerEnterEvent += OnTriggerEnterEvent;
        controller2D.onTriggerExitEvent += OnTriggerExitEvent;
        controller2D.onTriggerStayEvent += OnTriggerStayEvent;
    }

    private void Start()
    {
        currentSpeed = runSpeed;
    }

    private void OnControllerCollider(RaycastHit2D hit)
    {
        
    }

    private void OnTriggerEnterEvent(Collider2D col)
    {
       
    }

    private void OnTriggerStayEvent(Collider2D col)
    {

    }

    private void OnTriggerExitEvent(Collider2D col)
    {
        
    }

    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x == 1)
        {
            if (transform.localScale.x < 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (input.x == -1)
        {
            if (transform.localScale.x > 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        velocity = Vector2.Lerp(velocity, input * runSpeed, Time.deltaTime * groundDamping);
        controller2D.move(velocity * Time.deltaTime);
        velocity = controller2D.velocity;
    }
}
