using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAnimation : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private Transform startPoint;

    // Private Variables
    private float aimAngle;
    private bool enable = false;

    // Components
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;   
    }

    private void Update()
    {
        if (!enable) return;

        DrawAimingImage();
    }
    private void DrawAimingImage()
    {
        Vector2 arrowPos = cam.WorldToScreenPoint(startPoint.position);
        arrowPos = (Vector2)Input.mousePosition - arrowPos;

        aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;

        arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.2f);
        arrowImage.transform.position = (Vector2)startPoint.position + arrowPos;
        arrowImage.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    public void EnableBowImage()
    {
        enable = true;
        arrowImage.SetActive(true);
    }

    public void DisableBowImage()
    {
        enable = false;
        arrowImage.SetActive(false);
    }
}
