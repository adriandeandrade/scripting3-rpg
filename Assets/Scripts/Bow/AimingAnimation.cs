using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAnimation : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private Transform startPoint;

    // Private Variables
    private float aimAngle;

    // Components
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;   
    }

    private void Update()
    {
        DrawAimingImage();
    }

    public void DrawAimingImage()
    {
        Vector2 arrowPos = cam.WorldToScreenPoint(startPoint.position);
        arrowPos = (Vector2)Input.mousePosition - arrowPos;

        aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;

        arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.2f);
        arrowImage.transform.position = (Vector2)startPoint.position + arrowPos;
        arrowImage.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
}
