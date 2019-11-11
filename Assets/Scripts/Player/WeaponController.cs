using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enjoii.Characters;
using enjoii.Items;

public class WeaponController : MonoBehaviour
{
    // Inspector Fields
    [Header("Equipped Weapon Configuration")]
    [SerializeField] private GameObject equippedWeaponHolder;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Character character;
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private SpriteRenderer weaponImage;
    [SerializeField] private float offset;

    // Private Variables
    private Vector2 mousePos;
    private Vector2 objectPos;
    private Camera cam;
    private float angle;
    private bool attackIn = false;
    private bool swinging = false;

    private void Awake()
    {
        cam = Camera.main;
        character = GetComponent<Character>();
        character.OnItemEquipped += SetWeapon;
    }

    private void Update()
    {
        FaceAwayFromCursor();
        //UpdateAnimator();

        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            StartCoroutine(AttackRoutine());
            swinging = true;
        }
    }

    private IEnumerator AttackRoutine()
    {
        if (attackIn)
        {
            weaponAnimator.SetBool("AttackIn", false);
            attackIn = false;
        }
        else
        {
            weaponAnimator.SetBool("AttackIn", true);
            attackIn = true;
        }

        float time = weaponAnimator.GetCurrentAnimatorStateInfo(0).length;

        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        swinging = false;

        yield break;
    }

    public void SetWeapon(Item item)
    {
        weaponImage.sprite = item.ItemIcon;
    }

    private void FaceAwayFromCursor()
    {
        mousePos = Input.mousePosition;
        objectPos = cam.WorldToScreenPoint(equippedWeaponHolder.transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        angle = Mathf.Atan2(-mousePos.y, -mousePos.x) * Mathf.Rad2Deg;

        equippedWeaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }
}
