using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class FollowMouseUI : MonoBehaviour
{
    public event EventHandler OnZoomIn;
    public event EventHandler OnZoomOut;

    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private float targetFieldOfView = 80f;

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 localPoint);
        GetComponent<RectTransform>().localPosition = localPoint;

        playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * 2f);

        if (Input.GetMouseButtonDown(1))
        {
            //animator.SetBool("ScopeUp", true);
            Cursor.visible = false;
            targetFieldOfView = 50f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //animator.SetBool("ScopeUp", false);
            Cursor.visible = true;
            targetFieldOfView = 80f;
        }
    }


}
