using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;

public enum BowState
{
    IDLE,
    DRAWING,
    READY
}

[RequireComponent(typeof(BowEvents))]
public class Bow : MonoBehaviour, IWeapon
{
    // Inspector Fields
    [Header("Bow Configuration")]
    [SerializeField] private BowEvents bowEvents;
    [SerializeField] private int itemID;
    [SerializeField] private KeyCode drawBowKey = KeyCode.Space;

    [Header("Bow Graphics")]
    [SerializeField] private SpriteRenderer arrowGhostImage; // The sprite renderer for the arrow type that is currently equipped.
    [SerializeField] private Sprite arrowSprite; // DEBUG
    [SerializeField] private GameObject arrowPrefab; // DEBUG
    [SerializeField] private GameObject bowImage;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform bowTransform;

    // Private Variables
    private float aimAngle;
    private float nextDamage;
    private Item item;
    private BowState currentBowState;

    // Components
    private Animator bowAnimator;

    private bool equipped = false;

    // Components
    private Camera cam;
    private Player player;

    public int CurrentDamage { get; set; }
    public List<BaseStat> WeaponStats { get; set; }
    public int ItemID { get; set; }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        bowAnimator = GetComponent<Animator>();
        cam = Camera.main;

        bowEvents.OnBowDraw += OnBowFinishedDraw;
        bowEvents.OnBowReady += OnBowReset;

        OnWeaponEquipped();
    }

    private void Start()
    {
        item = GameManager.Instance.ItemDatabase.GetItem(itemID);
    }

    private void Update()
    {
        if (!equipped) return;

        UpdateState();
        RotateTowardsMouse();
    }

    private void SetState(BowState newState)
    {
        switch (newState)
        {
            case BowState.IDLE:
                currentBowState = BowState.IDLE;
                OnBowReset();
                break;

            case BowState.DRAWING:
                currentBowState = BowState.DRAWING;
                bowAnimator.SetBool("Drawing", true);
                break;

            case BowState.READY:
                currentBowState = BowState.READY;
                bowAnimator.SetBool("Ready", true);
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentBowState)
        {
            case BowState.IDLE:
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
    public void PerformAttack(int damage)
    {
        if(currentBowState == BowState.IDLE)
        {
            arrowGhostImage.sprite = arrowSprite;
            SetState(BowState.DRAWING);
        }
    }

    public void OnWeaponEquipped()
    {
        startPoint = transform;
        SetArrowImage();
        equipped = true;
        EnableBowImage();
    }

    private void ShootArrow()
    {
        Arrow newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
        newArrow.transform.parent = null;
        newArrow.transform.localScale = Vector3.one;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        newArrow.LaunchProjectile(direction, 25f, arrowSprite, 1);
        arrowGhostImage.sprite = null;
    }

    private void SetArrowImage()
    {
        if (arrowGhostImage != null)
        {
            arrowGhostImage.sprite = arrowSprite;
        }
    }

    private void RotateTowardsMouse()
    {
        Vector2 arrowPos = cam.WorldToScreenPoint(startPoint.position);
        arrowPos = (Vector2)Input.mousePosition - arrowPos;

        aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;

        arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.2f);

        bowTransform.transform.position = (Vector2)startPoint.position + arrowPos;
        bowTransform.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    public void EnableBowImage()
    {
        equipped = true;
        bowImage.SetActive(true);
    }

    public void DisableBowImage()
    {
        equipped = false;
        bowImage.SetActive(false);
    }

    private void OnBowFinishedDraw()
    {
        SetState(BowState.READY);
    }

    private void OnBowReset()
    {
        Debug.Log("Bow has been shot and has reset itself.");
        bowAnimator.ResetTrigger("Shoot");
        bowAnimator.SetBool("Drawing", false);
        bowAnimator.SetBool("Ready", false);
    }

    public void PerformSpecialAttack()
    {
        throw new System.NotImplementedException();
    }
}
