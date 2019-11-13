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

[RequireComponent(typeof(BowEvents))]
public class Bow : MonoBehaviour, IWeapon
{
    // Inspector Fields
    [Header("Bow Configuration")]
    [SerializeField] private BowEvents bowEvents;
    [SerializeField] private EquippableItem bowitem;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private KeyCode drawBowKey = KeyCode.Space;

    [Header("Bow Graphics")]
    [SerializeField] private SpriteRenderer arrowGhostImage;
    [SerializeField] private GameObject bowImage;
    [SerializeField] private Transform startPoint;

    // Private Variables
    private float aimAngle;
    private BowState currentBowState;
     // Used to get the prefab for the arrow

    // Components
    private Animator bowAnimator;

    private bool enable = false;

    // Components
    private Camera cam;
    private Player player;

    public int CurrentDamage { get; set; }
    public List<BaseStat> WeaponStats { get; set; }
    public EquippableItem EquippableItemData { get => bowitem; }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        bowAnimator = GetComponent<Animator>();
        cam = Camera.main;

        bowEvents.OnBowDraw += OnBowFinishedDraw;
        bowEvents.OnBowReady += OnBowReset;
    }
    private void Start()
    {
        startPoint = transform.parent;
        enable = true;
    }

    private void Update()
    {
        if (!enable) return;

        UpdateState();
        DrawAimingImage();
    }

    public void InitializeBow(Item item)
    {
        bowitem = item as EquippableItem;
        arrowGhostImage.sprite = bowitem.projectileIcon;
        enable = true;
        EnableBowImage();
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
                    arrowGhostImage.sprite = bowitem.projectileIcon;
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
        Arrow newArrow = Instantiate(bowitem.projectilePrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
        newArrow.transform.parent = null;
        newArrow.transform.localScale = Vector3.one;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        newArrow.LaunchProjectile(direction, 25f, bowitem.projectileIcon, 1);
        arrowGhostImage.sprite = null;
    }

    private void OnBowEquipped(EquippableItem item)
    {
        bowitem = item;
        arrowGhostImage.sprite = bowitem.projectileIcon;
        enable = true;
        EnableBowImage();
    }


    private void DrawAimingImage()
    {
        Vector2 arrowPos = cam.WorldToScreenPoint(startPoint.position);
        arrowPos = (Vector2)Input.mousePosition - arrowPos;

        aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;

        arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.2f);

        bowImage.transform.position = (Vector2)startPoint.position + arrowPos;
        bowImage.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    public void EnableBowImage()
    {
        enable = true;
        bowImage.SetActive(true);
    }

    public void DisableBowImage()
    {
        enable = false;
        bowImage.SetActive(false);
    }

    private void OnBowFinishedDraw()
    {
        SetState(BowState.READY);
    }

    private void OnBowReset()
    {
        Debug.Log("Bow has been shot and has reset itself.");
    }

    public void PerformAttack(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void PerformSpecialAttack()
    {
        throw new System.NotImplementedException();
    }
}
