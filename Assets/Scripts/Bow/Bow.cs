using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enjoii.Items;
using enjoii.Characters;
using UnityEngine.Rendering.PostProcessing;

public enum BowState
{
    IDLE,
    DRAWING,
    READY
}

public class Bow : MonoBehaviour
{
    // Inspector Fields
    [Header("Bow Configuration")]
    [SerializeField] private BowEvents bowEvents;
    [SerializeField] private SpriteRenderer arrowImage;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private Animator bowAnimator;
    [SerializeField] private BowAnimation bowAnimation;
    [SerializeField] private KeyCode drawBowKey = KeyCode.Space;

    // Private Variables
    private BowState currentBowState;
    private EquippableItem bowData; // Used to get the prefab for the arrow

    private bool enable = false;

    // Components
    private Camera cam;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        bowAnimation = GetComponent<BowAnimation>();
        cam = Camera.main;

        bowEvents.OnBowDraw += OnBowFinishedDraw;
        bowEvents.OnBowReady += OnBowReset;
        player.OnWeaponEquipped += OnBowEquipped;
        player.OnWeaponDequipped += OnBowUnequipped;
    }
    private void Start()
    {
        enable = false;
        bowAnimation.DisableBowImage();
        arrowImage.sprite = null;
    }

    private void Update()
    {
        if (!enable) return;

        UpdateState();
    }

    private void SetState(BowState newState)
    {
        switch (newState)
        {
            case BowState.IDLE:
                currentBowState = BowState.IDLE;
                bowAnimator.SetBool("Drawing", false);
                break;

            case BowState.DRAWING:
                currentBowState = BowState.DRAWING;
                bowAnimator.SetBool("Drawing", true);
                break;

            case BowState.READY:
                currentBowState = BowState.READY;
                bowAnimator.SetBool("Drawing", false);
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
                    bowAnimator.SetTrigger("CancelDraw");
                    SetState(BowState.IDLE);
                }

                break;

            case BowState.READY:
                if (Input.GetKeyUp(drawBowKey))
                {
                    Debug.Log("Shot");
                    bowAnimator.SetTrigger("Shoot");
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

        newArrow.LaunchProjectile(direction, 25f, bowData.projectileIcon, player.strengthStat.Value);
        arrowImage.sprite = null;
    }

    private void OnBowEquipped(EquippableItem item)
    {
        bowData = item;
        arrowImage.sprite = bowData.projectileIcon;
        enable = true;
        bowAnimation.EnableBowImage();
    }

    private void OnBowUnequipped(EquippableItem item)
    {
        bowData = null;
        enable = false;
        arrowImage.sprite = null;
        bowAnimation.DisableBowImage();
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
