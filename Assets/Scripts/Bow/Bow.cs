using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enjoii.Items;
using enjoii.Characters;

public enum BowState
{
    IDLE,
    DRAWING,
    READY
}

public class Bow : MonoBehaviour
{
    [SerializeField] private BowEvents bowEvents;
    [SerializeField] private EquippableItem bowData; // Used to get the prefab for the arrow
    [SerializeField] private SpriteRenderer arrowImage;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;
    [SerializeField] private KeyCode drawBowKey = KeyCode.Space;

    [SerializeField] private BowState currentBowState;

    private Camera cam;
    private bool isReady = false;

    private void Awake()
    {
        bowEvents.OnBowDraw += OnBowFinishedDraw;
        bowEvents.OnBowReady += OnBowReset;
        arrowImage.sprite = null;
        cam = Camera.main;
    }

    private void Update()
    {
        UpdateState();
    }

    private void SetState(BowState newState)
    {
        switch (newState)
        {
            case BowState.IDLE:
                currentBowState = BowState.IDLE;
                animator.SetBool("Drawing", false);
                break;

            case BowState.DRAWING:
                currentBowState = BowState.DRAWING;
                animator.SetBool("Drawing", true);
                break;

            case BowState.READY:
                currentBowState = BowState.READY;
                animator.SetBool("Drawing", false);
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentBowState)
        {
            case BowState.IDLE:
                if (Input.GetKeyDown(drawBowKey))
                {
                    arrowImage.sprite = bowData.projectileIcon;
                    SetState(BowState.DRAWING);
                }

                break;

            case BowState.DRAWING:
                if (Input.GetKeyUp(drawBowKey))
                {
                    animator.SetTrigger("CancelDraw");
                    SetState(BowState.IDLE);
                }
                break;

            case BowState.READY:
                if (Input.GetKeyUp(drawBowKey))
                {
                    Debug.Log("Shot");
                    animator.SetTrigger("Shoot");
                    ShootArrow();
                    SetState(BowState.IDLE);
                }
                break;
        }
    }

    private void ShootArrow()
    {
        Arrow newArrow = Instantiate(bowData.projectilePrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
        newArrow.transform.parent = null;
        newArrow.transform.localScale = Vector3.one;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        newArrow.LaunchProjectile(direction, 25f);
        arrowImage.sprite = null;
    }

    private void OnBowFinishedDraw()
    {
        SetState(BowState.READY);
    }

    private void OnBowReset()
    {
        Debug.Log("Bow has been shot and has reset itself.");
    }
}
